using Pozitron.Extensions.EntityFrameworkCore.Internals;
using Xunit;

namespace Pozitron.Extensions.EntityFrameworkCore.Tests.Internals;

public class TypeUtilTests
{
    private abstract class BaseType<T> { }
    private class DerivedType : BaseType<int> { }
    private class DeeplyDerivedType : DerivedType { }
    private class UnrelatedType { }

    [Theory]
    [InlineData(typeof(DerivedType), typeof(BaseType<>), true)]
    [InlineData(typeof(DeeplyDerivedType), typeof(BaseType<>), true)]
    [InlineData(typeof(UnrelatedType), typeof(BaseType<>), false)]
    [InlineData(typeof(object), typeof(BaseType<>), false)]
    public void IsDerived_ShouldReturnExpectedResult(Type objectType, Type mainType, bool expected)
    {
        // Act
        var result = TypeUtil.IsDerived(objectType, mainType);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(typeof(DerivedType), typeof(BaseType<>), typeof(BaseType<int>))]
    [InlineData(typeof(DeeplyDerivedType), typeof(BaseType<>), typeof(BaseType<int>))]
    [InlineData(typeof(UnrelatedType), typeof(BaseType<>), null)]
    [InlineData(typeof(object), typeof(BaseType<>), null)]
    public void GetBaseEnumType_ShouldReturnExpectedType(Type objectType, Type mainType, Type? expected)
    {
        // Act
        var result = TypeUtil.GetBaseEnumType(objectType, mainType);

        // Assert
        Assert.Equal(expected, result);
    }
}
