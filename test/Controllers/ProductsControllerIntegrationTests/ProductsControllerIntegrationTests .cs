using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using ProductApi.Application.DTOs.Requests;
using ProductApi.Application.DTOs.Responses;
using FluentAssertions;
using ProductApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProductApi.Tests.Controllers
{
    public class ProductsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public ProductsControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // services.Remove(...); // If needed
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseInMemoryDatabase("TestDb"));
                });
            });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Get_ShouldReturnAllProducts()
        {
            // Arrange
            var seedProduct = new CreateProductDto("Test Product 1", 99.99m);
            await _client.PostAsJsonAsync("/api/products", seedProduct);

            // Act
            var response = await _client.GetAsync("/api/products");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
            products.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_WithValidId_ShouldReturnProduct()
        {
            // Arrange
            var seedProduct = new CreateProductDto("Test Product 2", 50.00m);
            var postResponse = await _client.PostAsJsonAsync("/api/products", seedProduct);
            var createdId = await postResponse.Content.ReadFromJsonAsync<Guid>();

            // Act
            var response = await _client.GetAsync($"/api/products/{createdId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var product = await response.Content.ReadFromJsonAsync<ProductDto>();
            product.Should().NotBeNull();
            product.Name.Should().Be(seedProduct.Name);
        }

        [Fact]
        public async Task Post_ShouldCreateProduct()
        {
            // Arrange
            var newProduct = new CreateProductDto("Test Product 3", 75.00m);

            // Act
            var response = await _client.PostAsJsonAsync("/api/products", newProduct);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdId = await response.Content.ReadFromJsonAsync<Guid>();
            createdId.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Put_ShouldUpdateProduct()
        {
            // Arrange
            var seedProduct = new CreateProductDto("Test Product 4", 100.00m);
            var postResponse = await _client.PostAsJsonAsync("/api/products", seedProduct);
            var createdId = await postResponse.Content.ReadFromJsonAsync<Guid>();

            var updatedProduct = new UpdateProductDto("Updated Product 4", 120.00m);

            // Act
            var response = await _client.PutAsJsonAsync($"/api/products/{createdId}", updatedProduct);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var getResponse = await _client.GetAsync($"/api/products/{createdId}");
            var product = await getResponse.Content.ReadFromJsonAsync<ProductDto>();
            product.Name.Should().Be(updatedProduct.Name);
            product.Price.Should().Be(updatedProduct.Price);
        }

        [Fact]
        public async Task Delete_ShouldRemoveProduct()
        {
            // Arrange
            var seedProduct = new CreateProductDto("Test Product 5", 60.00m);
            var postResponse = await _client.PostAsJsonAsync("/api/products", seedProduct);
            var createdId = await postResponse.Content.ReadFromJsonAsync<Guid>();

            // Act
            var response = await _client.DeleteAsync($"/api/products/{createdId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var getResponse = await _client.GetAsync($"/api/products/{createdId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
