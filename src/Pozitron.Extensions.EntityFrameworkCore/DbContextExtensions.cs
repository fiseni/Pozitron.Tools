using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pozitron.Extensions.EntityFrameworkCore;

public static class DbContextExtensions
{
    public static void ApplyAuditing(this DbContext dbContext, IDateTime dateTime, ICurrentUser currentUser)
    {
        var addedEntries = dbContext.ChangeTracker.Entries<IAuditableEntity>().Where(x => x.IsAdded());
        var modifiedEntries = dbContext.ChangeTracker.Entries<IAuditableEntity>().Where(x => x.IsModified());

        var now = dateTime.Now;

        foreach (var entry in addedEntries)
        {
            entry.CurrentValues[nameof(IAuditableEntity.AuditCreatedTime)] = now;
            entry.CurrentValues[nameof(IAuditableEntity.AuditCreatedByUserId)] = currentUser.UserId;
            entry.CurrentValues[nameof(IAuditableEntity.AuditCreatedByUsername)] = currentUser.Username;
        }

        foreach (var entry in modifiedEntries)
        {
            entry.CurrentValues[nameof(IAuditableEntity.AuditModifiedTime)] = now;
            entry.CurrentValues[nameof(IAuditableEntity.AuditModifiedByUserId)] = currentUser.UserId;
            entry.CurrentValues[nameof(IAuditableEntity.AuditCreatedByUsername)] = currentUser.Username;
        }
    }

    public static void ApplySoftDelete(this DbContext dbContext)
    {
        List<EntityEntry>? ownedEntries = null;

        foreach (var entry in dbContext.ChangeTracker.Entries<ISoftDelete>())
        {
            if (entry.State != EntityState.Deleted) continue;

            //entry.CurrentValues.SetValues(entry.OriginalValues);
            entry.State = EntityState.Unchanged;
            entry.CurrentValues[nameof(ISoftDelete.IsDeleted)] = true;

            ownedEntries ??= dbContext.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Deleted && x.Metadata.IsOwned())
                .ToList();

            foreach (var ownedEntry in ownedEntries)
            {
                if (ownedEntry.Metadata.IsInOwnershipPath(entry.Metadata.ContainingEntityType))
                {
                    //ownedEntry.CurrentValues.SetValues(ownedEntry.OriginalValues);
                    ownedEntry.State = EntityState.Unchanged;
                }
            }
        }
    }

    public static async Task PublishDomainEvents(this DbContext dbContext, IMediator mediator)
    {
        var entries = dbContext.ChangeTracker.Entries<IDomainEventContainer>();

        foreach (var entry in entries)
        {
            var entity = entry.Entity;
            if (entity.Events.Count == 0) continue;

            var events = entity.Events.ToList();
            entity.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
