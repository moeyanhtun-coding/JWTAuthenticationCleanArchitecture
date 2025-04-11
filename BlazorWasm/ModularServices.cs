using Application.Services.Authentication;
using Blazored.LocalStorage;

namespace BlazorWasm
{
    public static class ModularServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IAccount, AccountService>();
            services.AddBlazoredLocalStorage();
            return services;
        }
    }
}
