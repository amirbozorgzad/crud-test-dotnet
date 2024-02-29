using Microsoft.OpenApi.Models;

namespace API.Swagger;

public class SwaggerStartupConfig
{
    public void AddSwaggerGen(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("CustomerAreaV1", new OpenApiInfo { Title = "Customer Area APIs", Version = "v1" });

            List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory,
                "*.xml",
                SearchOption.TopDirectoryOnly
            ).ToList();
            xmlFiles.ForEach(xmlFile => c.IncludeXmlComments(xmlFile));
        });
    }

    public void ConfigSwaggerUI(IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/CustomerAreaV1/swagger.json", "Customer Area v1"); });
    }
}