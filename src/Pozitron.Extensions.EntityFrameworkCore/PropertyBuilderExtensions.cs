namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TEnum> HasConversionToValue<TEnum>(this PropertyBuilder<TEnum> propertyBuilder)
        where TEnum : BaseEnum<TEnum, int>
    {
        return propertyBuilder.HasConversion(x => x.Value, x => BaseEnum<TEnum, int>.FromValue(x));
    }

    public static PropertyBuilder<TEnum?> HasConversionToValueOrNull<TEnum>(this PropertyBuilder<TEnum?> propertyBuilder)
        where TEnum : BaseEnum<TEnum, int>
    {
        return propertyBuilder.HasConversion(x => x.ToValueOrNull(), x => BaseEnum<TEnum, int>.FromValueOrNull(x));
    }

    public static PropertyBuilder<TEnum> HasConversionToName<TEnum>(this PropertyBuilder<TEnum> propertyBuilder, int? maxLength)
        where TEnum : BaseEnum<TEnum, int>
    {
        var propBuilder = propertyBuilder.HasConversion(x => x.Name, x => BaseEnum<TEnum, int>.FromName(x, false));

        if (maxLength.HasValue)
        {
            propBuilder.HasMaxLength(maxLength.Value);
        }

        return propBuilder;
    }

    public static PropertyBuilder<TEnum?> HasConversionToNameOrNull<TEnum>(this PropertyBuilder<TEnum?> propertyBuilder, int? maxLength)
        where TEnum : BaseEnum<TEnum, int>
    {
        var propBuilder = propertyBuilder.HasConversion(x => x.ToNameOrNull(), x => BaseEnum<TEnum, int>.FromNameOrNull(x, false));

        if (maxLength.HasValue)
        {
            propBuilder.HasMaxLength(maxLength.Value);
        }

        return propBuilder;
    }



    // For future investigation!
    // EF Core has no support for covariance in this case, it expects the exact type.
    // This is tried just for the sake of completeness. In reality we're always using `int` as TValue, so the above implementations satisfy our requirements.
    private static PropertyBuilder<BaseEnum<TEnum, TValue>> HasConversionToValue<TEnum, TValue>(this PropertyBuilder<BaseEnum<TEnum, TValue>> propertyBuilder)
        where TEnum : BaseEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        return propertyBuilder.HasConversion(x => x.Value, x => BaseEnum<TEnum, TValue>.FromValue(x));
    }

    private static PropertyBuilder<TBaseEnum> HasConversionToValue<TBaseEnum, TEnum, TValue>(this PropertyBuilder<TBaseEnum> propertyBuilder)
        where TBaseEnum : BaseEnum<TEnum, TValue>, TEnum
        where TEnum : BaseEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        return propertyBuilder.HasConversion(x => x.Value, x => (BaseEnum<TEnum, TValue>.FromValue(x) as TBaseEnum)!);
    }

    private static PropertyBuilder<TEnum> HasConversionToValue2<TEnum, TValue>(this PropertyBuilder<BaseEnum<TEnum, TValue>> propertyBuilder)
        where TEnum : BaseEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        return (propertyBuilder.HasConversion(x => x.Value, x => BaseEnum<TEnum, TValue>.FromValue(x)) as PropertyBuilder<TEnum>)!;
    }

    private static PropertyBuilder<BaseEnum<TEnum, TValue>> Property<TEntity, TEnum, TValue>(this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, BaseEnum<TEnum, TValue>>> propertyExpression)
        where TEntity : class
        where TEnum : BaseEnum<TEnum, TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        return builder.Property(propertyExpression);
    }
}
