using System.Text.Json.Serialization;
using API.Swagger;
using Core.Domain.Context;
using MC2.CrudTest.Core.Domain.Context;
using Mc2.CrudTest.Presentation.Api.Infrastructure;

namespace Mc2.CrudTest.Presentation.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        new DotNetStartupConfig().ServiceConfig(services);
        new SwaggerStartupConfig().AddSwaggerGen(services, Configuration);
        new DependencyInjectionStartupConfig(services, Configuration);
        services.AddMvc(option => option.EnableEndpointRouting = false)
            .AddJsonOptions(
                options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        new DotNetStartupConfig().AppEnvironmentConfig(app, env);
        new DotNetStartupConfig().AppConfig(app);
        new SwaggerStartupConfig().ConfigSwaggerUI(app, Configuration);
        app.Map("",
            builder => { builder.Run(async context => { context.Response.Redirect("/swagger/index.html"); }); });
        using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
        {
            CoreContext context = serviceScope.ServiceProvider.GetRequiredService<CoreContext>();
            DbInitializer.Initialize(context);
        }
    }
}