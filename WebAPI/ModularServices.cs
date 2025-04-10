using Application.Contract;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Repo;
namespace WebAPI
{
    public static class ModularServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, 
            WebApplicationBuilder builder, IConfiguration configuration)
        {
            services
                .AddDbContext(builder)
                .AddAuthentication(builder, configuration)
                .AddRepo();
            return services;
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

        public static IServiceCollection AddAuthentication(this IServiceCollection services, 
            WebApplicationBuilder builder, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
            });
            return services;
        }

        public static IServiceCollection AddRepo(this IServiceCollection services)
        {
            services.AddScoped<IUser, UserRepo>();
            return services;
        }
    }
}
