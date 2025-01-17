using FluentValidation;
//TODO : Use SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions as FluentValidation.AspNetCore is deprecated
//using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using FluentValidation.AspNetCore;
using MediatR.Extensions.FluentValidation.AspNetCore;
using MediatR;
using ProductApi.Application.Features.Products.Commands;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using ProductApi.Application.Mapping.Responses;
using ProductApi.Application.Services;
using ProductApi.Web.Settings;
using ProductApi.Infrastructure.Extensions;
using ProductApi.Domain.Interfaces;
using ProductApi.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));
var applicationSettings = builder.Configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>();

builder.AddNpgsqlDbContext<AppDbContext>(applicationSettings.DatabaseName);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(ProductDtoProfile).Assembly);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
    options.AddPolicy("SecurePolicy", policy =>
    {
        policy.WithOrigins("https://mytrusteddomain.com")
              .WithMethods("GET", "POST")
              .WithHeaders("Authorization", "Content-Type")
              .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
    });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateProductCommand>());

//builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidation(new[] { typeof(CreateProductCommandValidator).Assembly });
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatR.Extensions.FluentValidation.AspNetCore.ValidationBehavior<,>));

var app = builder.Build();

app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext httpContext) =>
{
    var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

    if (exception is ValidationException)
    {
        return Results.BadRequest(exception.Message);
    }
    else if (exception is Exception ex)
    {
        return Results.Problem(ex.Message);
    }
    return Results.Problem();
});

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseCors("AllowAll");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.CreateDbIfNotExistsAsync();

app.Run();

public partial class Program { }