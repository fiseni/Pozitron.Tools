using Xunit;

namespace Pozitron.SharedKernel.Tests;

public class AppExceptionTests
{
    [Fact]
    public void Constructor_WithMessage_SetsMessage()
    {
        // Arrange
        const string expectedMessage = "Test error message";

        // Act
        var exception = new AppException(expectedMessage);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_SetsMessageAndInnerException()
    {
        // Arrange
        const string expectedMessage = "Test error message";
        var innerException = new Exception("Inner exception");

        // Act
        var exception = new AppException(expectedMessage, innerException);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
        Assert.Same(innerException, exception.InnerException);
    }
}
