using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public static class ModularServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            return services.AddDbContext(builder);
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DbConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString), 
                ServiceLifetime.Transient, 
                ServiceLifetime.Transient);
            return services;
        }
    }
}
