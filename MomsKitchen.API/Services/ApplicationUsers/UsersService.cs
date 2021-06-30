using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MomsKitchen.API.Constants;
using MomsKitchen.API.Repositories;
using MomsKitchen.API.Services.Auth;
using MomsKitchen.DATA.DTO.ApplicationUsers;
using MomsKitchen.DATA.Entities;
using System;
using System.Threading.Tasks;

namespace MomsKitchen.API.Services.ApplicationUsers
{
    public class UsersService: ControllerService<ApplicationUser, PostUserRequest, UpdateUserRequest>, IUsersService
    {
        private readonly IMapper _mapper;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IServiceResponse<ApplicationUser> _response;

        public UsersService(
                UserManager<ApplicationUser> userManager,
                IRepository<ApplicationUser> repository,
                IServiceResponse<ApplicationUser> response,
                IMapper mapper,
                IAuthService authService) : base(repository, response, mapper, authService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _response = response;
        }

        public async Task<IServiceResponse<ApplicationUser>> GetUsers() => await GetAll();

        public async Task<IServiceResponse<ApplicationUser>> DeleteUser(Guid userId) => await Delete(userId);

        public async Task<IServiceResponse<ApplicationUser>> GetUser(Guid userId) => await Get(userId);

        public async Task<IServiceResponse<ApplicationUser>> GetByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return _response.Error();

            return _response.Successfull(user);
        }

        public async Task<IServiceResponse<ApplicationUser>> CreateUser(PostUserRequest request)
        {
            var userData = _mapper.Map<ApplicationUser>(request);

            userData = SetCreateProperties(userData);

            try
            {

                var creationResult = await _userManager.CreateAsync(userData, request.Password);

                if (!creationResult.Succeeded)
                    return _response.Error(ErrorMessages.ValidationError);

                await _userManager.AddToRoleAsync(userData, UserRoles.User);

                return _response.Successfull(userData);
            }
            catch (Exception ex)
            {
                return _response.Error(ex.Message);
            }
        }

        public async Task<IServiceResponse<ApplicationUser>> CreateAdmin(PostUserRequest request)
        {
            var userData = _mapper.Map<ApplicationUser>(request);

            userData = SetCreateProperties(userData);

            try
            {
                var creationResult = await _userManager.CreateAsync(userData, request.Password);

                if (!creationResult.Succeeded)
                    return _response.Error(ErrorMessages.ValidationError);

                await _userManager.AddToRoleAsync(userData, UserRoles.Admin);

                return _response.Successfull(userData);
            }
            catch (Exception ex)
            {
                return _response.Error(ex.Message);
            }
        }

        public async Task<IServiceResponse<ApplicationUser>> UpdateUser(Guid userId, UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null) return _response.Error(ErrorMessages.NotFound);

            user = _mapper.Map(request, user);

            user = SetUpdateProperties(user);

            try
            {
                var updateResult = await _userManager.UpdateAsync(user);

                if (updateResult.Succeeded) return _response.Successfull();

                return _response.Error();
            }
            catch (Exception ex)
            {
                return _response.Error(ex.Message);
            }
        }
    }
}
