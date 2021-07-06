using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MomsKitchen.API.Configuration;
using MomsKitchen.API.Constants;
using MomsKitchen.DATA.DTO.Auth;
using MomsKitchen.DATA.Entities;

namespace MomsKitchen.API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationSettings _appSettings;

        public AuthService(
            IOptions<ApplicationSettings> appSettings,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }

        public Guid? GetLoggedUserId()
        {
            var loggedUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            if (loggedUserId == null) return null;

            return Guid.Parse(loggedUserId);
        }

        public async Task<bool> ChangePassword(ChangePasswordRequest request)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var updateResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            return updateResult.Succeeded;
        }

        public async Task<object> CheckUser(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                throw new BadHttpRequestException(ErrorMessages.NotFound);

            var validUser = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!validUser)
                throw new BadHttpRequestException(ErrorMessages.ValidationError);

            var token = await GenerateJwtToken(user);

            return new { token };
        }

        public async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var tokenDescriptor = await SetTokenDescriptor(user);

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public async Task<string> GetUserRoles(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles.FirstOrDefault();
        }

        public async Task<SecurityTokenDescriptor> SetTokenDescriptor(ApplicationUser user)
        {
            var _roleOptions = new IdentityOptions();

            var secretKey = Encoding.UTF8.GetBytes(_appSettings.JWTSecretKey);

            return new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(_roleOptions.ClaimsIdentity.UserNameClaimType, user.Id.ToString()),
                    new Claim(_roleOptions.ClaimsIdentity.RoleClaimType, await GetUserRoles(user))
                }),

                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}