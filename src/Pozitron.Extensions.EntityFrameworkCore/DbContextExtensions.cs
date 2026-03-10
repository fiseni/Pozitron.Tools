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

        foreach (var addedEntry in addedEntries)
        {
            addedEntry.Entity.UpdateCreateInfo(now, currentUser);
        }

        foreach (var modifiedEntry in modifiedEntries)
        {
            modifiedEntry.Entity.UpdateModifyInfo(now, currentUser);
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
                    ownedEntry.CurrentValues.SetValues(ownedEntry.OriginalValues);
                    ownedEntry.State = EntityState.Unchanged;
                }
            }
        }
    }

    public static async Task PublishDomainEvents(this DbContext dbContext, IMediator mediator)
    {
        var entities = dbContext.ChangeTracker.Entries<IDomainEventContainer>().Select(x => x.Entity).Where(x => x.Events.Any()).ToList();

        foreach (var entity in entities)
        {
            var events = entity.Events.ToList();
            entity.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
