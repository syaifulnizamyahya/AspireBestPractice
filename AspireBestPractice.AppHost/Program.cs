using AspireBestPractice.AppHost.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedis("cache");

//var apiService = builder.AddProject<Projects.AspireBestPractice_ApiService>("apiservice");

//builder.AddProject<Projects.AspireBestPractice_Web>("webfrontend")
//    .WithExternalHttpEndpoints()
//    .WithReference(cache)
//    .WaitFor(cache)
//    .WithReference(apiService)
//    .WaitFor(apiService);

builder.Services.Configure<ProductApiSettings>(builder.Configuration.GetSection("ProductApiSettings"));
var productApiSettings = builder.Configuration.GetSection("ProductApiSettings").Get<ProductApiSettings>();

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithPgWeb();
var postgresdb = postgres.AddDatabase(productApiSettings.DatabaseName);

var productApiService = builder.AddProject<Projects.ProductApi_Web>("productapi-web")
    .WithExternalHttpEndpoints()
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.Build().Run();
