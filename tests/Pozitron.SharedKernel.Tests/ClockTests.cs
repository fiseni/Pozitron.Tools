using Xunit;

namespace Pozitron.SharedKernel.Tests;

public class ClockTests
{
    [Fact]
    public void Initialize_SetsProvider()
    {
        // Arrange
        Clock.Initialize();

        // Assert
        Assert.NotNull(Clock.Provider);
    }

    [Fact]
    public void Initialize_WithCustomProvider_SetsCustomProvider()
    {
        // Arrange
        var customProvider = new TestDateTimeProvider();
        var expectedTime = new DateTimeOffset(2025, 1, 1, 12, 0, 0, TimeSpan.Zero);

        // Act
        Clock.Initialize(customProvider);

        // Assert
        Assert.Equal(expectedTime, Clock.Now);
    }

    private class TestDateTimeProvider : IDateTime
    {
        public DateTimeOffset Now => new DateTimeOffset(2025, 1, 1, 12, 0, 0, TimeSpan.Zero);
    }
}
