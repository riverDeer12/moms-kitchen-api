using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MomsKitchen.DATA.DTO.Recipes;

namespace MomsKitchen.API.Services
{
    public interface IRecipesService
    {
        Task<ICollection<RecipeDetails>> GetRecipes();
        Task<RecipeDetails> GetRecipe(Guid recipeId);
        Task<bool> DeleteRecipe(Guid recipeId);
        Task<bool> CreateRecipe(RecipeRequest request);
        Task<bool> UpdateRecipe(Guid recipeId, RecipeRequest request); 
    }
}
