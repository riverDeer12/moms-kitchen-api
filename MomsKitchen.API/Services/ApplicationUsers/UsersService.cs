using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MomsKitchen.API.Constants;
using MomsKitchen.API.Repositories;
using MomsKitchen.API.Services.Auth;
using MomsKitchen.DATA.DTO.ApplicationUsers;
using MomsKitchen.DATA.Entities;
using MomsKitchen.DATA.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MomsKitchen.API.Services.ApplicationUsers
{
    public class UsersService: ControllerService<ApplicationUser, UserRequest, UserRequest>, IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService(
                UserManager<ApplicationUser> userManager,
                IRepository<ApplicationUser> repository,
                IMapper mapper,
                IAuthService authService) : base(repository, mapper, authService)
        {
            _userManager = userManager;
        }

        public async Task<List<ApplicationUserDetails>> GetUsers()
        {
            var users = await _repository.GetAll();

            return _mapper.Map<List<ApplicationUserDetails>>(users);
        }

        public async Task<bool> DeleteUser(Guid userId) => await Delete(userId);

        public async Task<ApplicationUserDetails> GetUser(Guid userId)
        {
            var user = await _repository.Find(userId);

            return _mapper.Map<ApplicationUserDetails>(user);
        }

        public async Task<ApplicationUserDetails> GetByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                throw new NotFoundException(ErrorMessages.NotFound);

            return _mapper.Map<ApplicationUserDetails>(user);
        }

        public async Task<bool> CreateUser(UserRequest request)
        {
            var userData = _mapper.Map<ApplicationUser>(request);

            userData = SetCreateProperties(userData);

            try
            {

                var creationResult = await _userManager.CreateAsync(userData, request.Password);

                if (!creationResult.Succeeded)
                    throw new BadRequestException(ErrorMessages.ErrorSaving);

                await _userManager.AddToRoleAsync(userData, UserRoles.User);

                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> CreateAdmin(UserRequest request)
        {
            var userData = _mapper.Map<ApplicationUser>(request);

            userData = SetCreateProperties(userData);

            try
            {
                var creationResult = await _userManager.CreateAsync(userData, request.Password);

                if (!creationResult.Succeeded)
                    throw new BadRequestException(ErrorMessages.ValidationError);

                await _userManager.AddToRoleAsync(userData, UserRoles.Admin);

                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> UpdateUser(Guid userId, UserRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                throw new BadRequestException(ErrorMessages.NotFound);

            user = _mapper.Map(request, user);

            user = SetUpdateProperties(user);

            try
            {
                var updateResult = await _userManager.UpdateAsync(user);

                if (!updateResult.Succeeded)
                    throw new BadRequestException(ErrorMessages.ErrorUpdating);

                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
