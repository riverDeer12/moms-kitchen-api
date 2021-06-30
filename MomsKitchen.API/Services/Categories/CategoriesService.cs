using System;
using System.Threading.Tasks;
using AutoMapper;
using MomsKitchen.API.Repositories;
using MomsKitchen.API.Services.Auth;
using MomsKitchen.DATA.DTO.Categories;
using MomsKitchen.DATA.Entities;

namespace MomsKitchen.API.Services.Categories
{
    public class CategoriesService : ControllerService<Category, PostCategoryRequest, UpdateCategoryRequest>, ICategoriesService
    {
        public CategoriesService(
        IRepository<Category> repository,
        IServiceResponse<Category> response,
        IMapper mapper,
        IAuthService authService):base(repository, response, mapper, authService){}

        public async Task<IServiceResponse<Category>> GetCategories() => await GetAll();

        public async Task<IServiceResponse<Category>> GetCategory(Guid categoryId) => await Get(categoryId);

        public async Task<IServiceResponse<Category>> DeleteCategory(Guid categoryId) => await Delete(categoryId);

        public async Task<IServiceResponse<Category>> CreateCategory(PostCategoryRequest request) => await Create(request);

        public async Task<IServiceResponse<Category>> UpdateCategory(Guid categoryId, UpdateCategoryRequest request) => await Update(categoryId, request);
    }
}