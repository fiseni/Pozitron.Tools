using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pozitron.Extensions.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static void ConfigureCustomRules(this ModelBuilder modelBuilder, DbContext dbContext, Assembly? assembly = null)
    {
        // The order is important

        if (assembly is not null)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        modelBuilder.Ignore<DomainEvent>();

        modelBuilder.ConfigureTablesAndOwnedTypes(dbContext);

        modelBuilder.ConfigureSoftDelete();
    }

    private static void ConfigureTablesAndOwnedTypes(this ModelBuilder modelBuilder, DbContext dbContext)
    {
        var dbSetNames = dbContext.GetType().GetProperties()
            .Where(x => x.PropertyType.IsGenericType && typeof(DbSet<>).IsAssignableFrom(x.PropertyType.GetGenericTypeDefinition()))
            .Select(x => x.Name)
            .ToList();

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var isRootType = entityType.GetRootType() == entityType;

            if (!entityType.IsOwned())
            {
                ConfigureTableNames(entityType, dbSetNames);

                if (isRootType && typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    ConfigureTemporal(entityType);
                }
            }
            else if (!entityType.IsMappedToJson())
            {
                ConfigureOwnedTypeNavigations(entityType);

                if (isRootType)
                {
                    ConfigureTemporalForOwnedType(entityType, entityType);
                }
            }
        }
    }

    private static void ConfigureSoftDelete(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var isRootType = entityType.GetRootType() == entityType;

            if (isRootType && typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.Name, x => x.Property(nameof(ISoftDelete.IsDeleted)));
                entityType.AddSoftDeleteQueryFilter();
            }
        }
    }

    private static void ConfigureTableNames(IMutableEntityType entityType, List<string> dbSetNames)
    {
        var tableName = entityType.GetTableName();
        var defaultTableName = entityType.GetDefaultTableName();

        if (tableName is null || defaultTableName is null) return;
        if (tableName.Equals(defaultTableName)) return;

        if (dbSetNames.Find(x => x.Equals(tableName)) is not null)
        {
            entityType.SetTableName(entityType.DisplayName());
        }
    }

    private static void ConfigureOwnedTypeNavigations(IMutableEntityType entityType)
    {
        var ownership = entityType.FindOwnership();

        if (ownership is null) return;

        if (ownership.IsUnique)
        {
            ownership.IsRequiredDependent = true;
        }
    }

    private static void ConfigureTemporalForOwnedType(IMutableEntityType entityType, IMutableEntityType parentEntityType)
    {
        var ownership = parentEntityType.FindOwnership();

        if (ownership is null) return;

        var parent = ownership.PrincipalEntityType;

        if (parent is null) return;

        if (parent.IsOwned())
        {
            ConfigureTemporalForOwnedType(entityType, parent);
        }
        else if (typeof(IAuditableEntity).IsAssignableFrom(parent.ClrType))
        {
            ConfigureTemporal(entityType);
        }
    }

    private static void ConfigureTemporal(IMutableEntityType entityType)
    {
        entityType.SetIsTemporal(true);

        var periodStart = entityType.FindDeclaredProperty("PeriodStart");
        if (periodStart is not null)
            periodStart.SetColumnName("PeriodStart");

        var periodEnd = entityType.FindDeclaredProperty("PeriodEnd");
        if (periodEnd is not null)
            periodEnd.SetColumnName("PeriodEnd");
    }

    private static void AddSoftDeleteQueryFilter(this IMutableEntityType entityData)
    {
        var methodToCall = typeof(ModelBuilderExtensions)
            .GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)!
            .MakeGenericMethod(entityData.ClrType);

        var filter = methodToCall.Invoke(null, Array.Empty<object>());

        entityData.SetQueryFilter((LambdaExpression?)filter);
        entityData.AddIndex(entityData.FindProperty(nameof(ISoftDelete.IsDeleted))!);
    }

    private static LambdaExpression GetSoftDeleteFilter<TEntity>() where TEntity : class, ISoftDelete
    {
        Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
        return filter;
    }
}
