using CrudTest.Core.Context.CustomerContext;
using CrudTest.Core.Context.Model;

namespace CrudTest.Core;

public class DbInitializer
{
    public static void Initialize(CustomerContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Customer.Any())
        {
            Customer currency = new();
            context.SaveChanges();
        }
    }
}