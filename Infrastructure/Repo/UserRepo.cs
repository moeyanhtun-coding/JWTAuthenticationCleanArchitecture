using Application.Contract;
using Application.Models.Login;
using Application.Models.Register;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repo
{
    public class UserRepo : IUser
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public UserRepo(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<LoginResponseModel> LoginUserAsync(LoginUserModel loginUser)
        {
            var getUser = await FindUserByEmail(loginUser.Email!);
            if (getUser is null) return new LoginResponseModel(false, "User Not Found, Sorry!");
            var checkPassword = BCrypt.Net.BCrypt.Verify(loginUser.Password, getUser.Password);
            if (checkPassword)
                return new LoginResponseModel(true, "Login Successfully", GenerateJWTToken(getUser));
            else 
                return new LoginResponseModel(false, "Credentials are not valid");
        }


        public async Task<RegisterResponseModel> RegisterUserAsync(RegisterUserModel registerUser)
        {
            var getUser = await FindUserByEmail(registerUser.Email!);
            if (getUser is not null) return new RegisterResponseModel(false, "User Already Exist");
            var newUser = new ApplicationUser
            {
                Name = registerUser.Name,
                Email = registerUser.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUser.Password)
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return new RegisterResponseModel(true, "User Created Successfully");
        }

        private async Task<ApplicationUser> FindUserByEmail(string email)
            => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        private string GenerateJWTToken(ApplicationUser user)
        {
            var key = _configuration["Jwt:Key"]; // get from appsettings
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim("Id", user.Id.ToString()),
        new Claim("Name", user.Name!),
        new Claim("Email", user.Email!)
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
