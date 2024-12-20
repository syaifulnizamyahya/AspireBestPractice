var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.AspireBestPractice_ApiService>("apiservice");

builder.AddProject<Projects.AspireBestPractice_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.ProductApi_Web>("productapi-web");

builder.Build().Run();
