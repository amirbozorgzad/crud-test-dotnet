using CrudTest.Core.Context.Model;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Core.Context.CustomerContext;

public class CustomerContext : DbContext, ICustomerContext
{
    public CustomerContext(DbContextOptions<CustomerContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customer { get; set; }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

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