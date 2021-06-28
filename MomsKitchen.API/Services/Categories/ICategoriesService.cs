using System.Threading.Tasks;
using MomsKitchen.DATA.DTO.Categories;
using MomsKitchen.DATA.Entities;

namespace MomsKitchen.API.Services
{
    public interface ICategoriesService
    {
        Task<IServiceResponse<Category>> GetCategories();
        Task<IServiceResponse<Category>> GetCategory(string categoryId);
        Task<IServiceResponse<Category>> DeleteCategory(string categoryId);
        Task<IServiceResponse<Category>> CreateCategory(PostCategoryRequest request);
        Task<IServiceResponse<Category>> UpdateCategory(string categoryId, UpdateCategoryRequest request); 
    }
}