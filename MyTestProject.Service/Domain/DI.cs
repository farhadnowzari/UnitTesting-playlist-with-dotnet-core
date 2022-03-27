using Microsoft.EntityFrameworkCore;

namespace MyTestProject.Service.Domain;

public static class DI
{
    public static IServiceCollection AddDomainDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("main"));
        });

        return services;
    }
}