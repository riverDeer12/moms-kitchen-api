using Microsoft.AspNetCore.Mvc;
using MomsKitchen.API.Services.ApplicationUsers;
using MomsKitchen.API.Services.Auth;
using MomsKitchen.DATA.DTO.ApplicationUsers;
using MomsKitchen.DATA.DTO.Auth;
using System.Threading.Tasks;

namespace MomsKitchen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        private readonly IUsersService _usersService;

        public AuthController(IAuthService authService, IUsersService usersService)
        {
            _authService = authService;
            _usersService = usersService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _authService.CheckUser(request);

            if (!response.Success) return Unauthorized(response);

            var token = await _authService.GenerateJwtToken(response.Result);

            return Ok(new { token });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(PostUserRequest request)
        {
            var response = await _usersService.CreateUser(request);

            if (!response.Success) return BadRequest(response);

            var token = await _authService.GenerateJwtToken(response.Result);

            return Ok(new { token });
        }
    }
}
