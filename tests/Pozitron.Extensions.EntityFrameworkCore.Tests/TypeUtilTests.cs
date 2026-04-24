using Xunit;
using Pozitron.Extensions.EntityFrameworkCore;

namespace Pozitron.Extensions.EntityFrameworkCore.Tests;

public class TypeUtilTests
{
    // Testable types
    private class BaseGeneric<T> { }
    private class DerivedGeneric<T> : BaseGeneric<T> { }
    private class GrandchildGeneric<T> : DerivedGeneric<T> { }

    private class SimpleBase { }
    private class SimpleDerived : SimpleBase { }

    // For the 'currentType is null' scenario
    // In C#, a class always has a base (at least object).
    // However, we can use a type that doesn't have a verifiable hierarchy if we use reflection or specific edge cases,
    // but for the purpose of coverage, testing against 'object' is key.

    [Fact]
    public void IsDerived_ReturnsTrue_WhenTypeIsDerivedFromGeneric()
    {
        // Arrange
        var objectType = typeof(DerivedGeneric<int>);
        var mainType = typeof(BaseGeneric<>);

        // Act
        var result = TypeUtil.IsDerived(objectType, mainType);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsDerived_ReturnsTrue_WhenTypeIsDeeplyDerivedFromGeneric()
    {
        // Arrange
        var objectType = typeof(GrandchildGeneric<double>);
        var mainType = typeof(BaseGeneric<>);

        // Act
        var result = TypeUtil.IsDerived(objectType, mainType);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsDerived_ReturnsFalse_WhenTypeIsNotDerivedFromGeneric()
    {
        // Arrange
        var objectType = typeof(SimpleDerived);
        var mainType = typeof(BaseGeneric<>);

        // Act
        var result = TypeUtil.IsDerived(objectType, mainType);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetBaseEnumType_ReturnsType_WhenTypeIsDerivedFromGeneric()
    {
        // Arrange
        var objectType = typeof(DerivedGeneric<string>);
        var mainType = typeof(BaseGeneric<>);

        // Act
        var result = TypeUtil.GetBaseEnumType(objectType, mainType);

        // Assert
        Assert.Equal(typeof(BaseGeneric<string>), result);
    }

    [Fact]
    public void GetBaseEnumType_ReturnsType_WhenTypeIsDeeplyDerivedFromGeneric()
    {
        // Arrange
        var objectType = typeof(GrandchildGeneric<int>);
        var mainType = typeof(BaseGeneric<>);

        // Act
        var result = TypeUtil.GetBaseEnumType(objectType, mainType);

        // Assert
        // The logic returns the currentType that matches the mainType, which is BaseGeneric<int>
        Assert.Equal(typeof(BaseGeneric<int>), result);
    }

    [Fact]
    public void GetBaseEnumType_ReturnsNull_WhenTypeIsNotDerivedFromGeneric()
    {
        // Arrange
        var objectType = typeof(SimpleDerived);
        var mainType = typeof(BaseGeneric<>);

        // Act
        var result = TypeUtil.GetBaseEnumType(objectType, mainType);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IsDerived_ReturnsFalse_WhenBaseTypeIsObject()
    {
        // Arrange
        // objectType.BaseType for typeof(object) is null in some environments or doesn't exist in a way that hits the loop
        // But for any class, the loop runs until it hits object.
        var objectType = typeof(object);
        var mainType = typeof(BaseGeneric<>);

        // Act
        var result = TypeUtil.IsDerived(objectType, mainType);

        // Assert
        Assert.False(result);
    }
}
