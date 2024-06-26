using CrudTest.Core;
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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<CustomerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(CustomerContext).Assembly.FullName));
});

builder.Services.AddScoped<ICustomerContext>(provider => provider.GetService<CustomerContext>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddValidatorsFromAssembly(typeof(CustomerValidator<AddCustomerCommandModel>).Assembly);
builder.Services.AddTransient<IRequestHandler<AddCustomerCommandModel, long>, AddCustomerCommandHandler>();
builder.Services.AddTransient<IRequestHandler<EditCustomerCommandModel, long>, EditCustomerCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteCustomerCommandModel, long>, DeleteCustomerCommandHandler>();
builder.Services
    .AddTransient<IRequestHandler<GetAllCustomersQueryModel, IEnumerable<Customer>>, GetAllCustomersQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetCustomerByIdQueryModel, Customer>, GetCustomerByIdQueryHandler>();

// Register validators for each command model
builder.Services.AddTransient<IValidator<AddCustomerCommandModel>, CustomerValidator<AddCustomerCommandModel>>();
builder.Services.AddTransient<IValidator<EditCustomerCommandModel>, CustomerValidator<EditCustomerCommandModel>>();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Crud Test App"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CrudTestApp.WebApi"));
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CustomerContext>();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();