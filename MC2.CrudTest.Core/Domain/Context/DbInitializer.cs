using MC2.CrudTest.Core.Domain.Context;
using MC2.CrudTest.Core.Domain.Model;

namespace Core.Domain.Context;

public class DbInitializer
{
    public static void Initialize(CoreContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Customer.Any())
        {
            Customer currency = new();
            context.SaveChanges();
        }
    }
}