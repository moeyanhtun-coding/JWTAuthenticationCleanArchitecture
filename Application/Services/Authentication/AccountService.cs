using Application.Models.Login;
using Application.Models.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authentication
{
    public class AccountService : IAccount
    {
        private readonly HttpClient httpClient;

        public AccountService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<LoginResponseModel> LoginAccountAsync(LoginUserModel model)
        {
            var response = await httpClient.PostAsJsonAsync("api/auth/login", model);
            var result = await response.Content.ReadFromJsonAsync<LoginResponseModel>();
            return result!;
        }

        public async Task<RegisterResponseModel> RegisterAccountAsync(RegisterUserModel model)
        {
           var response = await httpClient.PostAsJsonAsync("api/auth/register", model);
            var result = await response.Content.ReadFromJsonAsync<RegisterResponseModel>();
            return result!;
        }
    }
}
