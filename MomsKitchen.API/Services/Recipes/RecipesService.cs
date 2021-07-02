using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MomsKitchen.API.Repositories;
using MomsKitchen.API.Services.Auth;
using MomsKitchen.DATA.DTO.Recipes;
using MomsKitchen.DATA.Entities;

namespace MomsKitchen.API.Services.Recipes
{
    public class RecipesService : ControllerService<Recipe, PostRecipeRequest, UpdateRecipeRequest>, IRecipesService
    {
        public RecipesService(
        IRepository<Recipe> repository,
        IMapper mapper,
        IAuthService authService):base(repository, mapper, authService){}

        public async Task<List<RecipeDetails>> GetRecipes() 
        { 
            var recipes = await GetAll();

            return _mapper.Map<List<RecipeDetails>>(recipes);
        } 

        public async Task<RecipeDetails> GetRecipe(Guid recipeId)
        {
            var recipe = await Get(recipeId);

            return _mapper.Map<RecipeDetails>(recipe);
        }

        public async Task<bool> DeleteRecipe(Guid recipeId) => await Delete(recipeId);

        public async Task<bool> CreateRecipe(PostRecipeRequest request) => await Create(request);

        public async Task<bool> UpdateRecipe(Guid recipeId, UpdateRecipeRequest request) => await Update(recipeId, request);
    }
}