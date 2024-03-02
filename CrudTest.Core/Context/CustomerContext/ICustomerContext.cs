using CrudTest.Core.Context.Model;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Core.Context.CustomerContext;

public interface ICustomerContext
{
    public DbSet<Customer> Customer { get; set; }

    Task<int> SaveChangesAsync();
}