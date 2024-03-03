using CrudTest.Core.Context.CustomerContext;
using Mc2.CrudTest.AcceptanceTests.Drivers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SolidToken.SpecFlow.DependencyInjection;

namespace Mc2.CrudTest.AcceptanceTests.Hooks;

[Binding]
public class LifecycleHook
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        ServiceCollection services = new();
        services.AddScoped<ICustomerDriver, CustomerDriver>();
        services.AddScoped<TestContext>();
        services.AddDbContextFactory<CustomerContext>(options =>
            options.UseSqlServer(
                "Data Source=.;Initial Catalog=TestDb;Integrated Security=true;TrustServerCertificate=True"));
        return services;
    }
}