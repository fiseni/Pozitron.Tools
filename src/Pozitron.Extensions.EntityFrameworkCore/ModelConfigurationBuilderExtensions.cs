using Microsoft.EntityFrameworkCore;

namespace Pozitron.Extensions.EntityFrameworkCore;

public static class ModelConfigurationBuilderExtensions
{
    public static void ConfigurePozitronConventions(this ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ConfigureBaseEnums();

        configurationBuilder.Properties<string>().HaveMaxLength(250);
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }

    public static void ConfigureBaseEnums(this ModelConfigurationBuilder configurationBuilder)
    {
        var modelBuilder = configurationBuilder.CreateModelBuilder(null);
        var propertyTypes = modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.ClrType.GetProperties())
            .Where(p => TypeUtil.IsDerived(p.PropertyType, typeof(BaseEnum<,>)))
            .Select(p => p.PropertyType)
            .Distinct();

        foreach (var propertyType in propertyTypes)
        {
            var baseEnumType = TypeUtil.GetBaseEnumType(propertyType, typeof(BaseEnum<,>));

            if (baseEnumType is null) continue;

            var baseType = baseEnumType.GenericTypeArguments[0];
            var keyType = baseEnumType.GenericTypeArguments[1];

            if (baseType is null || keyType is null) continue;

            var converterType = typeof(BaseEnumConverter<,,>).MakeGenericType(propertyType, baseType, keyType);

            configurationBuilder
                .Properties(propertyType)
                .HaveConversion(converterType);
        }
    }
}
