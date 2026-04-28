using Xunit;

namespace Pozitron.SharedKernel.Tests;

public class BaseEntityTests
{
    private class TestEntity : BaseEntity { }
    private class TestDomainEvent : DomainEvent { }

    [Fact]
    public void BaseEntity_InitializesWithDefaultValues()
    {
        // Arrange & Act
        var entity = new TestEntity();

        // Assert
        Assert.False(entity.IsDeleted);
        Assert.Null(entity.AuditCreatedTime);
        Assert.Null(entity.AuditCreatedByUserId);
        Assert.Null(entity.AuditCreatedByUsername);
        Assert.Null(entity.AuditModifiedTime);
        Assert.Null(entity.AuditModifiedByUserId);
        Assert.Null(entity.AuditModifiedByUsername);
        Assert.Empty(entity.Events);
    }

    [Fact]
    public void RegisterEvent_AddsEventToEventsList()
    {
        // Arrange
        var entity = new TestEntity();
        var domainEvent = new TestDomainEvent();

        // Act
        entity.RegisterEvent(domainEvent);

        // Assert
        Assert.Single(entity.Events);
        Assert.Equal(domainEvent, entity.Events.First());
    }

    [Fact]
    public void ClearDomainEvents_ClearsEventsList()
    {
        // Arrange
        var entity = new TestEntity();
        entity.RegisterEvent(new TestDomainEvent());
        entity.RegisterEvent(new TestDomainEvent());

        // Act
        entity.ClearDomainEvents();

        // Assert
        Assert.Empty(entity.Events);
    }
}
