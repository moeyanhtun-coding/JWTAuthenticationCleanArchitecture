using Application.Contract;
using Application.Models.Login;
using Application.Models.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUser _user;

        public AuthController(IUser user)
        {
            _user = user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseModel>> UserLogin(LoginUserModel loginUser)
        {
           var result = await _user.LoginUserAsync(loginUser);
            if (result.Flag)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseModel>> UserRegister(RegisterUserModel registerUser)
        {
            var result = await _user.RegisterUserAsync(registerUser);
            if (result.Flag)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
