using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorWasm.State
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private const string LocalStorageKey = "auth";
        private readonly ILocalStorageService localStorageService;
        private readonly ClaimsPrincipal anonymous = new (new ClaimsIdentity());
        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await localStorageService.GetItemAsStringAsync(LocalStorageKey);
            if(string.IsNullOrEmpty(token)) return await Task.FromResult(new AuthenticationState(anonymous));
        }
    }
}
