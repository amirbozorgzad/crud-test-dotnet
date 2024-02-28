using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Mc2.CrudTest.Presentation.Api.Infrastructure;

public class DotNetStartupConfig
{
    public void ServiceConfig(IServiceCollection services)
    {
        services.AddControllers(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.MaxDepth = 32;
            })
            .ConfigureApiBehaviorOptions(option => option.SuppressMapClientErrors = true);
        services.AddRazorPages();
        services.AddServerSideBlazor();
    


        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }

    public void AppEnvironmentConfig(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseHsts();
        }
    }

    public void AppConfig(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseCors();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        app.UseHttpsRedirection();
        app.UseHttpsRedirection();
        app.UseAuthorization();
    }
}