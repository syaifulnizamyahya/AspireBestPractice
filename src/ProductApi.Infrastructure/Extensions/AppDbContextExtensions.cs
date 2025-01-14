using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Initialization;

namespace ProductApi.Infrastructure.Extensions
{
    public static class AppDbContextExtensions
    {
        public static async Task CreateDbIfNotExistsAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<AppDbContext>>();
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                if (await context.Database.EnsureCreatedAsync())
                {
                    logger.LogInformation("Database created successfully.");
                }
                else
                {
                    logger.LogInformation("Database already exists.");
                }

                await DbInitializer.InitializeAsync(context, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }
    }
}
