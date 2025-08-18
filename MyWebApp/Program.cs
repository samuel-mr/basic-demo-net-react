using Application.Sales.Checkout;
using Application.Sales.Checkout.Policy;
using Domain.Core.Orders.Repositories;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MyWebApp;
using MyWebApp.Infra;
using Serilog;

var bld = WebApplication.CreateBuilder();
bld.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

bld.Services.CustomProblemDetails();
bld.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add Entity Framework with SQLite in-memory database
bld.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlite("Data Source=MySuperDatabase.db"));

// Register DDD services
bld.Services.AddScoped<IUserRepository,UserRepository>();
bld.Services.AddScoped<IOrderRepository, OrderRepository>();
bld.Services.AddScoped<IProductRepository, ProductRepository>();

// process
bld.Services.AddScoped<OrderProcessingService>();

// validation for policies
bld.Services.AddScoped<RateLimiterForPurchaseValidation>();
bld.Services.AddScoped<MaximumItemsPerClientValidation>();
bld.Services.AddScoped<AvailableUserValidation>();


// policies
bld.Services.AddScoped<CheckoutPolicy>();

bld.Services.AddFastEndpoints()
    .SwaggerDocument();

bld.Services.AddCors();

var app = bld.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "https://localhost:3000"));

app.UseFastEndpoints()
    .UseSwaggerGen();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.Run();