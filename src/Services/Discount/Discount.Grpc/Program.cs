using AutoMapper;
using Discount.API.Extensions;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;
using Npgsql;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container, dependencies and middlewares
builder.Services.AddGrpc();

builder.Services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString")));
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


var app = builder.Build();

app.MigrateDatabase<Program>();
// Configure the GreeterService as a gRPC service endpoint.
app.MapGrpcService<DiscountService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
