using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using Shouldly;
using System.Linq;
using System;

namespace efcore.readonlycollection.tests
{
    public class ReadonlyCollectionTests
    {
        readonly TestDbContext _context;

        public ReadonlyCollectionTests()
        {
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            _context = new TestDbContext(options);
            _context.Seed();
            _context.SaveChanges();
            _context.RootEntites.ShouldNotBeEmpty();
        }

        [Fact]
        public void WhereWithIReadonlyColection_should_work()
        {
            var obj = _context.RootEntites
                .Select(t => new { Values = t.ChildEntites.Where(c => c.Value != "first") })
                .First();

            obj.Values.Count().ShouldBe(1);
        }

        [Fact]
        public void WhereWithIReadonlyColection_by_extensionMethod_should_work_but_will_fail_anyway()
        {
            var obj = _context.RootEntites
                .Select(t => new { Values = t.ChildEntites.Filter() })
                .First();
            obj.Values.Count().ShouldBe(1);
        }
    }
    public static class DatabaseQueryExtensions
    {
        public static IEnumerable<ChildEntity> Filter(this IEnumerable<ChildEntity> source) =>
            source.Where(c => c.Value != "first");

    }
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
        public DbSet<RootEntity> RootEntites { get; set; }
        public DbSet<ChildEntity> ChildEntities { get; set; }

        public void Seed()
        {
            RootEntites.Add(new RootEntity
            {
                Id = 1
            });
            ChildEntities.Add(new ChildEntity() { Value = "first", Id = 1, RootEntityId = 1 });
            ChildEntities.Add(new ChildEntity() { Value = "second", Id = 2, RootEntityId = 1 });
        }
    }
    public class ChildEntity
    {
        public int Id { get; set; }
        public int RootEntityId { get; set; }
        public RootEntity RootEntity { get; set; }
        public string Value { get; set; }
    }
    public class RootEntity
    {
        public int Id { get; set; }
        public IReadOnlyCollection<ChildEntity> ChildEntites { get; set; }
    }
}
