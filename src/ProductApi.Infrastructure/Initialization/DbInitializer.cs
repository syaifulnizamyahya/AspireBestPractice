using Microsoft.Extensions.Logging;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Data;

namespace ProductApi.Infrastructure.Initialization
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context, ILogger<AppDbContext> logger)
        {
            if (context.Products.Any())
            {
                logger.LogInformation("Database already contains data, skipping seeding.");
                return;
            }

            logger.LogInformation("Seeding initial data...");

            var products = new[]
            {
                new Product("Product 1", 100),
                new Product("Product 2", 200),
                new Product("Product 3", 300),
                new Product("Product 4", 400),
                new Product("Product 5", 500),
            };

            try
            {
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
                logger.LogInformation("Initial data seeded successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
    }
}
