# Building enterprise class web API using ASP.NET Core with .NET 9.0
---

 <details>

 <summary>Click for screenshots</summary>

<!--## Screenshots-->
[Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview) main orchestration page
![Aspire1](images/Aspire1.png)
[Scalar](https://scalar.com/) Web Api Documentation page
![Web Api1](images/WebApi1.png)
Docker Desktop 
![Docker1](images/Docker1.png)
Get all products
![Getall](images/getall.png)
Get a product by Id
![Getbyid](images/Getbyid.png)
Create a product
![Create Product](images/CreateProduct.png)
Update a product
![Update](images/Update.png)
Delete a product
![Delete](images/Delete.png)
Console logs
![Console Logs](images/consoleLogs.png)
Structure logs
![Structure Logs](images/structureLogs.png)
Traces
![Traces1](images/Traces1.png)
![Traces2](images/Traces2.png)
Metrics
![Metrics](images/Metrics.png)
pgAdmin
![Pg Admin](images/pgAdmin.png)
pgWeb
![Pg Web](images/pgWeb.png)
 --- 

</details>

## Objective 

The primary objective of this project is to develop an enterprise-class Web API using ASP.NET Core with .NET 9.0, adhering to industry-leading practices and leveraging cutting-edge technologies. The project will be structured and implemented following a robust foundation built on:  

- [X] **Clean Architecture** and **Domain-Driven Design (DDD)** for maintainable and scalable solutions.  
- [X] **CQRS (Command Query Responsibility Segregation)** for clear separation of concerns.  
- [X] **Repository Pattern** and **Unit of Work** for efficient data access and transactional consistency.  
- [X] **Mediator Pattern** for streamlined communication between components.  
- [X] **Fluent Validation** for clean and reusable validation logic.  
- [X] Global **exception handling** to ensure resilience and reliability.  
- [X] Advanced **logging** for diagnostics and monitoring.  
- [X] **API Versioning** for backward compatibility and smooth evolution.  
- [ ] **Response Caching** to enhance performance.  
- [X] **Health Checks** to monitor application status.  
- [X] **Entity Framework Core** for robust ORM capabilities with **SQL Server** as the database.  
- [X] **AutoMapper** for object-to-object mapping.  
- [X] **FluentAssertions**, **Moq**, and **xUnit** for effective unit testing and ensuring code quality.  
- [ ] **Audit executing operations** for tracking changes.
- [X] **Scalar/OpenAPI** for API documentation and client consumption.  
- [X] **Docker** for containerization and portability.  
- [ ] **GitHub Actions** and **Azure DevOps** for CI/CD pipelines and deployment automation.  

This combination of design principles, frameworks, and tools will ensure the API is robust, scalable, testable, and production-ready.  

--- 

## Overview
 - The project will be a simple CRUD operation for a product entity.
 - The project will have the following operations:
   - Get all products
   - Get a product by Id
   - Create a product
   - Update a product
   - Delete a product
 - The product entity should has the following properties:
   ```csharp
   public class Product
   {
	   public int Id { get; set; }
	   public string Name { get; set; }
	   public decimal Price { get; set; }
   }
   ```
 - The project should looks something like this
![Target Overview](images/TargetOverview.png)
 - As of now, this project does not cover securing your Web Api. For securing enterprise-class Web API, check out [OWASP](https://cheatsheetseries.owasp.org/cheatsheets/DotNet_Security_Cheat_Sheet.html).

 --- 

 ## Running the project

You need the following installed locally:
- .NET 9.0
- Docker Desktop 
- Visual Studio 2022

Open the solution in Visual Studio 2022 and run the project. The project will be accessible here [https://localhost:17244/](https://localhost:17244/).

Scalar Web API documentation is accessible here [https://localhost:7203/scalar/v1](https://localhost:7203/scalar/v1).

pgAdmin is accessible here [http://localhost:56416/](http://localhost:56416/)

pgWeb is accessible here [http://localhost:56414/](http://localhost:56414/)

Health check is accessible by appending /health eg. [https://localhost:7203/health](https://localhost:7203/health)

Alive check is accessible by appending /alive eg. [https://localhost:7203/alive](https://localhost:7203/alive)

Do note that the port number might vary.

 --- 

## Basic project features
- Aspire orchestration features
	- Listing of Aspire resources
![Aspire Resource Listing](images/AspireResourceListing.png)
	- Console logs of each resources
![Aspire Console Logs](images/AspireConsoleLogs.png)
	- Structured view of logs for each projects
![Aspire Structured Logs](images/AspireStructuredLogs.png)
	- Traces
![Aspire Traces](images/AspireTraces.png)
	- Metrics
![Aspire Metrics](images/AspireMetrics.png)
- Scalar Web API documentation features
	- Get all products
![Getall](images/getall.png)
	- Get a product by Id
![Getbyid](images/Getbyid.png)
	- Create a product
![Create Product](images/CreateProduct.png)
	- Update a product
![Update](images/Update.png)
	- Delete a product
![Delete](images/Delete.png)
	- Models information
![Models Info](images/ModelsInfo.png)
- Product model class
[Product.cs](src/ProductApi.Domain/Entities/Product.cs)
- Controller
[Products Controller](src/ProductApi.Web/Controllers/V1/ProductsController.cs)
- Service
[ProductService.cs](src/ProductApi.Application/Services/ProductService.cs)

 --- 

## Technology And Best Practices
- [X] Leverage .NET Aspire for orchestrating distributed applications
	- [X] Monitoring
	- [X] Logging

- [X] Leverage Scalar for API documentation

- [X] Clean Architecture
	- [X] Presentation (ProductApi.Web)
	- [X] Application (ProductApi.Application)
	- [X] Domain (ProductApi.Domain)
	- [X] Infrastructure (ProductApi.Infrastructure)

- [X] Domain Driven Design
	- [X] Domain logic in Product entity

	```csharp
	public class Product : Entity
	{
		public string Name { get; private set; }
		public decimal Price { get; private set; }

		public Product(string name, decimal price)
		{
			Name = name;
			Price = price;
		}

		public void Update(string name, decimal price)
		{
			Name = name;
			Price = price;
		}
	```

	- [X] Application logic in ProductService

	```csharp
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetProductsAsync();
		Task<Product> GetProductByIdAsync(int id);
	```

- [X] Repository Pattern
	- [X] Generic Repository
	```csharp
	public class ProductRepository : Repository<Product>, IProductRepository
	```

- [X] [Unit Of Work](src/ProductApi.Infrastructure/UnitOfWork/UnitOfWork.cs)

- [X] Data Transfer Object
	- [X] Uses record
	```csharp
	public record CreateProductDto(string Name, decimal Price);
	```
	- [X] Separated Request and Response
	```
	- ProductApi.Application
	  - DTOs
		- Requests
		  - CreateProductDto.cs
		  - UpdateProductDto.cs
		- Responses
		  - ProductDto.cs
	```

- [X] Unit tests
	- [X] Uses xUnit 
	- [X] Uses Moq
	- [X] Uses FluentAssertions
	- [X] Arrange, Act, Assert pattern
	- [X] [Product Service Tests](test/Services/ProductServiceTests/ProductServiceTests.cs)

- [ ] Integration tests

- [X] CQRS (Command Query Responsibility Segregation)
	- [X] Uses MediatR 
		- [X] [Create Product Command](src/ProductApi.Application/Features/Products/Commands/CreateProductCommand.cs)
		- [X] [Create Product Command Handler](src/ProductApi.Application/Features/Products/Commands/CreateProductCommandHandler.cs)

- [X] Mapping
	- [X] Uses AutoMapper
		- [X] [Product Dto Profile](src/ProductApi.Application/Mapping/Responses/ProductDtoProfile.cs)

- [X] Fluent Validation
	- [X] DTO validation
		- [X] [Create Product Dto Validator](src/ProductApi.Application/Validators/CreateProductDtoValidator.cs)
	- [X] Command validation
		- [X] [Create Product Command Validator](src/ProductApi.Application/Features/Products/Commands/CreateProductCommandValidator.cs)

- [X] Global Exception Handling
	- [X] [Exception Handling Middleware](src/ProductApi.Web/Middlewares/ExceptionHandlingMiddleware.cs)

- [X] API Versioning
	- [X] Controller API versioning
	```csharp
	namespace ProductApi.Web.Controllers.V1
	{
		[ApiController]
		[ApiVersion("1.0")]
		[Route("api/v{version:apiVersion}/[controller]")]
		public class ProductsController : ControllerBase

	```
	- [X] Scalar API documentation
	```csharp
	builder.Services
		.AddApiVersioning(options =>
		{
			options.ReportApiVersions = true;
			options.AssumeDefaultVersionWhenUnspecified = true;
			options.DefaultApiVersion = new ApiVersion(1, 0);
		})
		.AddApiExplorer(options =>
		{
			options.GroupNameFormat = "'v'VVV";
			options.SubstituteApiVersionInUrl = true;
		});	
	```

- [ ] Response Caching  
	- [ ] Uses Redis
	- [ ] Hybrid caching

- [X] Entity Framework Core  
	- [X] Uses PostgreSQL
	- [X] Docker instance
	- [X] Integrated with pgAmin
	- [X] Integrated wiht pgWeb
	```csharp
	var postgres = builder.AddPostgres("postgres")
		.WithPgAdmin()
		.WithPgWeb();
	var postgresdb = postgres.AddDatabase(productApiSettings.DatabaseName);

	var productApiService = builder.AddProject<Projects.ProductApi_Web>("productapi-web")
		.WithExternalHttpEndpoints()
		.WithReference(postgresdb)
		.WaitFor(postgresdb);

	```

	```csharp
	builder.AddNpgsqlDbContext<AppDbContext>(applicationSettings.DatabaseName);

	```

- [X] API Documentation
	- [X] Uses Scalar/OpenAPI  
	```csharp
	if (app.Environment.IsDevelopment())
	{
		app.MapOpenApi();
		app.MapScalarApiReference();
		app.UseCors("AllowAll");
	}

	```

- [X] Containerization 
	- [X] Uses Docker  

- [ ] CI/CD pipelines and deployment automation

- [ ] Audit executing operations
	- [ ] Uses Audit.NET