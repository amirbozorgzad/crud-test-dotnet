using System.Reflection;
using CrudTest.Core.Context.CustomerContext;
using CrudTest.Core.Context.Model;
using CrudTest.Feature.CustomerFeatures.Command.Add;
using CrudTest.Feature.CustomerFeatures.Command.Delete;
using CrudTest.Feature.CustomerFeatures.Command.Edit;
using CrudTest.Feature.CustomerFeatures.Query.GetCustemerById;
using CrudTest.Feature.CustomerFeatures.Query.GetCustomerList;
using CrudTest.Feature.CustomerFeatures.Validator;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CustomerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(CustomerContext).Assembly.FullName));
});

builder.Services.AddScoped<ICustomerContext>(provider => provider.GetService<CustomerContext>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
builder.Services.AddValidatorsFromAssemblies(assemblies);
builder.Services.AddValidatorsFromAssemblyContaining<AddCustomerValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EditCustomerValidator>();

builder.Services.AddTransient<IRequestHandler<AddCustomerCommandModel, long>, AddCustomerCommandHandler>();
builder.Services.AddTransient<IRequestHandler<EditCustomerCommandModel, long>, EditCustomerCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteCustomerCommandModel, long>, DeleteCustomerCommandHandler>();
builder.Services
    .AddTransient<IRequestHandler<GetAllCustomersQueryModel, IEnumerable<Customer>>, GetAllCustomersQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetCustomerByIdQueryModel, Customer>, GetCustomerByIdQueryHandler>();


#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Crud Test App"
    });
});

#endregion

WebApplication app = builder.Build();

#region Swagger

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "CrudTestApp.WebApi"); });

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();