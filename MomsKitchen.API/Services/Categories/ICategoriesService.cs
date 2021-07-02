using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MomsKitchen.DATA.DTO.Categories;
using MomsKitchen.DATA.Entities;

namespace MomsKitchen.API.Services
{
    public interface ICategoriesService
    {
        Task<List<CategoryDetails>> GetCategories();
        Task<CategoryDetails> GetCategory(Guid categoryId);
        Task<bool> DeleteCategory(Guid categoryId);
        Task<bool> CreateCategory(PostCategoryRequest request);
        Task<bool> UpdateCategory(Guid categoryId, UpdateCategoryRequest request); 
    }
}