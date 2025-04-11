using Application.Models.Login;
using Application.Models.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authentication
{
    public interface IAccount
    {
        Task<RegisterResponseModel> RegisterAccountAsync(RegisterUserModel model);
        Task<LoginResponseModel> LoginAccountAsync(LoginUserModel model);
    }
}
