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
        base.OnModelCreating(modelBuilder);
    }
}