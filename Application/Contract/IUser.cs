using Application.Models.Register;
using Application.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract
{
    public interface IUser
    {
        Task<RegisterResponseModel> RegisterUserAsync(RegisterUserModel registerUser);
        Task<LoginResponseModel> LoginUserAsync(LoginUserModel loginUser);
    }
}
