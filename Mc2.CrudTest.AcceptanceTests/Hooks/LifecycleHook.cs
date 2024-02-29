using FluentValidation;
using Mc2.CrudTest.AcceptanceTests.Drivers;
using Mc2.CrudTest.Core.Contract.Customer;
using MC2.CrudTest.Core.Contract.Validator;
using MC2.CrudTest.Core.Domain;
using MC2.CrudTest.Core.Domain.Context;
using Mc2.CrudTest.Persistence;
using MC2.CrudTest.Service.Abstraction;
using MC2.CrudTest.Service.Implementation;
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
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IValidator<CustomerDto>, CustomerValidator>();
        services.AddScoped<ICustomerDriver, CustomerDriver>();
        services.AddScoped<TestContext>();
        services.AddDbContextFactory<CoreContext>(options =>
            options.UseSqlServer(
                "Data Source=.;Initial Catalog=TestDb;Integrated Security=true;TrustServerCertificate=True"));
        return services;
    }
}