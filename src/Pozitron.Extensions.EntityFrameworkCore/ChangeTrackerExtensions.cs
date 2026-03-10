using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pozitron.Extensions.EntityFrameworkCore;

public static class ChangeTrackerExtensions
{
    public static bool IsAdded(this EntityEntry entry) =>
        entry.State == EntityState.Added;

    public static bool IsModified(this EntityEntry entry) =>
        entry.State != EntityState.Added &&
        (entry.State == EntityState.Modified ||
         entry.References.Any(r => r.TargetEntry is not null &&
                                   r.TargetEntry.Metadata.IsOwned() &&
                                   (r.TargetEntry.State == EntityState.Added ||
                                    r.TargetEntry.State == EntityState.Modified)));

    public static bool IsDeleted(this EntityEntry entry) =>
        entry.State == EntityState.Deleted;

    public static bool IsAddedModifiedDeleted(this EntityEntry entry) =>
        entry.State == EntityState.Added ||
        entry.State == EntityState.Deleted ||
        (entry.State == EntityState.Modified ||
         entry.References.Any(r => r.TargetEntry is not null &&
                                   r.TargetEntry.Metadata.IsOwned() &&
                                   (r.TargetEntry.State == EntityState.Added ||
                                    r.TargetEntry.State == EntityState.Modified)));
}
