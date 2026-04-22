using Microsoft.EntityFrameworkCore;

namespace Pozitron.Extensions.EntityFrameworkCore.Tests;

public class ChangeTrackerExtensionsTests
{
    private class TestContext : DbContext
    {
        public DbSet<TestEntity> Entities => Set<TestEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TestDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestEntity>().OwnsOne(x => x.Owned);
        }
    }

    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public OwnedEntity? Owned { get; set; }
    }

    public class OwnedEntity
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public int TestEntityId { get; set; }
    }

    [Fact]
    public void IsAdded_ShouldReturnTrue_WhenStateIsAdded()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);

        var entry = context.Entry(entity);
        entry.IsAdded().Should().BeTrue();
    }

    [Fact]
    public void IsAdded_ShouldReturnFalse_WhenStateIsNotAdded()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);
        context.SaveChanges();

        var entry = context.Entry(entity);
        entry.IsAdded().Should().BeFalse();
    }

    [Fact]
    public void IsModified_ShouldReturnTrue_WhenStateIsModified()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);
        context.SaveChanges();
        entity.Name = "Modified";

        var entry = context.Entry(entity);
        entry.IsModified().Should().BeTrue();
    }

    [Fact]
    public void IsModified_ShouldReturnTrue_WhenOwnedEntityIsAdded()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);
        context.SaveChanges();
        entity.Owned = new OwnedEntity { Value = "Owned" };

        var entry = context.Entry(entity);
        entry.IsModified().Should().BeTrue();
    }

    [Fact]
    public void IsModified_ShouldReturnTrue_WhenOwnedEntityIsModified()
    {
        using var context = new TestContext();
        var entity = new TestEntity
        {
            Name = "Test",
            Owned = new OwnedEntity { Value = "Owned" }
        };
        context.Entities.Add(entity);
        context.SaveChanges();
        entity.Owned.Value = "Modified";

        var entry = context.Entry(entity);
        entry.IsModified().Should().BeTrue();
    }

    [Fact]
    public void IsModified_ShouldReturnFalse_WhenStateIsAdded()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);
        entity.Name = "Modified";

        var entry = context.Entry(entity);
        entry.IsModified().Should().BeFalse();
    }

    [Fact]
    public void IsModified_ShouldReturnFalse_WhenStateIsAddedAndOwnedEntityIsAdded()
    {
        using var context = new TestContext();
        var entity = new TestEntity
        {
            Name = "Test",
            Owned = new OwnedEntity { Value = "Owned" }
        };
        context.Entities.Add(entity);

        var entry = context.Entry(entity);
        entry.IsModified().Should().BeFalse();
    }

    [Fact]
    public void IsModified_ShouldReturnFalse_WhenStateIsUnchanged()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);
        context.SaveChanges();

        var entry = context.Entry(entity);
        entry.IsModified().Should().BeFalse();
    }

    [Fact]
    public void IsAddedOrModified_ShouldReturnFalse_WhenStateIsUnchanged()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);
        context.SaveChanges();

        var entry = context.Entry(entity);
        entry.IsAddedOrModified().Should().BeFalse();
    }

    [Fact]
    public void IsAddedOrModified_ShouldReturnTrue_WhenStateIsAdded()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);

        var entry = context.Entry(entity);
        entry.IsAddedOrModified().Should().BeTrue();
    }

    [Fact]
    public void IsAddedOrModified_ShouldReturnTrue_WhenStateIsModified()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);
        context.SaveChanges();
        entity.Name = "Modified";

        var entry = context.Entry(entity);
        entry.IsAddedOrModified().Should().BeTrue();
    }

    [Fact]
    public void IsAddedOrModifiedOrDeleted_ShouldReturnFalse_WhenStateIsUnchanged()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);
        context.SaveChanges();

        var entry = context.Entry(entity);
        entry.IsAddedOrModifiedOrDeleted().Should().BeFalse();
    }

    [Fact]
    public void IsAddedOrModifiedOrDeleted_ShouldReturnTrue_WhenStateIsDeleted()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);
        context.SaveChanges();
        context.Remove(entity);

        var entry = context.Entry(entity);
        entry.IsAddedOrModifiedOrDeleted().Should().BeTrue();
    }

    [Fact]
    public void IsDeleted_ShouldReturnTrue_WhenStateIsDeleted()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);
        context.SaveChanges();
        context.Remove(entity);

        var entry = context.Entry(entity);
        entry.IsDeleted().Should().BeTrue();
    }

    [Fact]
    public void IsDeleted_ShouldReturnFalse_WhenStateIsNotDeleted()
    {
        using var context = new TestContext();
        var entity = new TestEntity { Name = "Test" };
        context.Entities.Add(entity);

        var entry = context.Entry(entity);
        entry.IsDeleted().Should().BeFalse();
    }
}
