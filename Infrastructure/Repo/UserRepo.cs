using Application.Contract;
using Application.Models.Login;
using Application.Models.Register;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    internal class UserRepo : IUser
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoginResponseModel> LoginUserAsync(LoginUserModel loginUser)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginUser.Email);
            if (getUser is null) return new LoginResponseModel(false, "User Not Found, Sorry!");
            var checkPassword = BCrypt.Net.BCrypt.Verify(loginUser.Password, getUser.Password);

;        }
        public Task<RegisterResponseModel> RegisterUserAsync(RegisterUserModel registerUser)
        {
            throw new NotImplementedException();
        }
    }
}
