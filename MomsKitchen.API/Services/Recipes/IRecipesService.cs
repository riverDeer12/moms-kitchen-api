using System.Threading.Tasks;
using MomsKitchen.DATA.DTO.Recipes;
using MomsKitchen.DATA.Entities;

namespace MomsKitchen.API.Services
{
    public interface IRecipesService
    {
        Task<IServiceResponse<Recipe>> GetRecipes();
        Task<IServiceResponse<Recipe>> GetRecipe(string recipeId);
        Task<IServiceResponse<Recipe>> DeleteRecipe(string recipeId);
        Task<IServiceResponse<Recipe>> CreateRecipe(PostRecipeRequest request);
        Task<IServiceResponse<Recipe>> UpdateRecipe(string recipeId, UpdateRecipeRequest request); 
    }
}
