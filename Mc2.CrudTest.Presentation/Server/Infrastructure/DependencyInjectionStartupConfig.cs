using FluentValidation;
using Mc2.CrudTest.Core.Contract.Customer;
using MC2.CrudTest.Core.Contract.Validator;
using MC2.CrudTest.Core.Domain;
using MC2.CrudTest.Core.Domain.Context;
using Mc2.CrudTest.Persistence;
using MC2.CrudTest.Service.Abstraction;
using MC2.CrudTest.Service.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Presentation.Api.Infrastructure;

public class DependencyInjectionStartupConfig
{
    public DependencyInjectionStartupConfig(IServiceCollection services, IConfiguration configuration)
    {
        SetupDbContexts(services, configuration);
        SetupServices(services);
    }


    private static void SetupDbContexts(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("ApplicationDbContext") ??
                                  "Server=.;Initial Catalog=TestDb;Encrypt=False;TrustServerCertificate=true;Trusted_Connection=True;";
        services.AddDbContext<CoreContext>(options => options
            .UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void SetupServices(IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IValidator<CustomerDto>, CustomerValidator>();
    }
}