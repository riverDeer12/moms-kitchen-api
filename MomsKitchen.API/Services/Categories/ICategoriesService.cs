using System;
using System.Threading.Tasks;
using MomsKitchen.DATA.DTO.Categories;
using MomsKitchen.DATA.Entities;

namespace MomsKitchen.API.Services
{
    public interface ICategoriesService
    {
        Task<IServiceResponse<Category>> GetCategories();
        Task<IServiceResponse<Category>> GetCategory(Guid categoryId);
        Task<IServiceResponse<Category>> DeleteCategory(Guid categoryId);
        Task<IServiceResponse<Category>> CreateCategory(PostCategoryRequest request);
        Task<IServiceResponse<Category>> UpdateCategory(Guid categoryId, UpdateCategoryRequest request); 
    }
}