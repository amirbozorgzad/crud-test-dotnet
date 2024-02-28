using FluentValidation;
using MC2.CrudTest.Core.Contract.Validator;
using MC2.CrudTest.Core.Domain.Context;
using MC2.CrudTest.Core.Domain.Model;
using MC2.CrudTest.Service.Abstraction;
using MC2.CrudTest.Service.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Presentation.API;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();


        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddDbContext<CoreContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext")));

        services.AddEndpointsApiExplorer();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IValidator<Customer>, CustomerValidator>();
    }
}