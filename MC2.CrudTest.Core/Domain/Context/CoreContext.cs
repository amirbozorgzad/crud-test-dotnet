using MC2.CrudTest.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace MC2.CrudTest.Core.Domain.Context;
#nullable disable
public class CoreContext : DbContext
{
    public CoreContext()
    {
    }

    public CoreContext(DbContextOptions<CoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customer { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");
            entity.HasKey(c => c.Id);
            entity.HasIndex(c => new { c.FirstName, c.LastName, c.DateOfBirth }).IsUnique();
        });
    }
}