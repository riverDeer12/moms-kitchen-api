using System;
using System.Collections.Generic;
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
        IMapper mapper,
        IAuthService authService):base(repository, mapper, authService){}

        public async Task<List<CategoryDetails>> GetCategories()
        {
            var categories = await _repository.GetAll();

            return _mapper.Map<List<CategoryDetails>>(categories);
        }

        public async Task<CategoryDetails> GetCategory(Guid categoryId)
        {
            var category = await _repository.Find(categoryId);

            return _mapper.Map<CategoryDetails>(category);
        }

        public async Task<bool> DeleteCategory(Guid categoryId) => await Delete(categoryId);

        public async Task<bool> CreateCategory(PostCategoryRequest request) => await Create(request);

        public async Task<bool> UpdateCategory(Guid categoryId, UpdateCategoryRequest request) => await Update(categoryId, request);
    }
}