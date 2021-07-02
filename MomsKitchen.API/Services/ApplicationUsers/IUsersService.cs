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
        Task<List<ApplicationUserDetails>> GetUsers();
        Task<ApplicationUserDetails> GetUser(Guid userId);
        Task<bool> DeleteUser(Guid userId);
        Task<bool> CreateUser(PostUserRequest request);
        Task<bool> CreateAdmin(PostUserRequest request);
        Task<bool> UpdateUser(Guid userId, UpdateUserRequest request);
        Task<ApplicationUserDetails> GetByUserName(string username);
    }
}
