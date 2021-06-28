using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MomsKitchen.DATA.DTO.Auth;
using MomsKitchen.DATA.Entities;

namespace MomsKitchen.API.Services.Auth
{
    public interface IAuthService
    {
        Guid? GetLoggedUserId();
        Task<IServiceResponse<ApplicationUser>> CheckUser(LoginRequest request);
        Task<string> GenerateJwtToken(ApplicationUser user);
        Task<SecurityTokenDescriptor> SetTokenDescriptor(ApplicationUser user);
        Task<string> GetUserRoles(ApplicationUser user);
        Task<bool> ChangePassword(ChangePasswordRequest request);
    }
}