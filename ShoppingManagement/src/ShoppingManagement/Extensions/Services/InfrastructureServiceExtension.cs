namespace ShoppingManagement.Extensions.Services;

using ShoppingManagement.Databases;
using ShoppingManagement.Resources;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        // DbContext -- Do Not Delete
        if (env.IsEnvironment(LocalConfig.FunctionalTestingEnvName) || env.IsDevelopment())
        {
            services.AddDbContext<ShoppingDbContext>(options =>
                options.UseInMemoryDatabase($"ShoppingManagementDb"));
        }
        else
        {
            services.AddDbContext<ShoppingDbContext>(options =>
                options.UseNpgsql(
                    Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "placeholder-for-migrations",
                    builder => builder.MigrationsAssembly(typeof(ShoppingDbContext).Assembly.FullName))
                            .UseSnakeCaseNamingConvention());
        }

        // Auth -- Do Not Delete
    }
}
