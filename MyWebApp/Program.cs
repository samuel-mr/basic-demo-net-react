using Application.Sales.Checkout;
using Application.Sales.Checkout.Policy;
using Domain.Core.Orders.Repositories;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MyWebApp;
using MyWebApp.Helpers;
using MyWebApp.Infra;
using Serilog;

var bld = WebApplication.CreateBuilder();
bld.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

bld.Services.ConfigureExceptions();
bld.Services.ConfigureDatabase();

bld.Services.RegisterRepositories();
bld.Services.RegisterProcess();
bld.Services.RegisterValidationPolicies();
bld.Services.RegisterPolicies();
bld.Services.ConfigureApis();

var app = bld.Build();

app.UseApis();
app.UseExceptions();

app.Run();