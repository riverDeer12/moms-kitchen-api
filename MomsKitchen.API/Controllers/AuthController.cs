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
            => Ok(await _authService.CheckUser(request));

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRequest request) 
            => Ok(await _usersService.CreateUser(request));
    }
}
