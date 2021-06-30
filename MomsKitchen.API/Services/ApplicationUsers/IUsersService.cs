using MomsKitchen.DATA.DTO.ApplicationUsers;
using MomsKitchen.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MomsKitchen.API.Services.ApplicationUsers
{
    public interface IUsersService
    {
        Task<IServiceResponse<ApplicationUser>> GetUsers();
        Task<IServiceResponse<ApplicationUser>> GetUser(Guid userId);
        Task<IServiceResponse<ApplicationUser>> DeleteUser(Guid userId);
        Task<IServiceResponse<ApplicationUser>> CreateUser(PostUserRequest request);
        Task<IServiceResponse<ApplicationUser>> CreateAdmin(PostUserRequest request);
        Task<IServiceResponse<ApplicationUser>> UpdateUser(Guid userId, UpdateUserRequest request);
        Task<IServiceResponse<ApplicationUser>> GetByUserName(string username);
    }
}
