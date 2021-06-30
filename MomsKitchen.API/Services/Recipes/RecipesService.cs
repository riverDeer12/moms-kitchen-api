using System;
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
        IServiceResponse<Recipe> response,
        IMapper mapper,
        IAuthService authService):base(repository, response, mapper, authService){}

        public async Task<IServiceResponse<Recipe>> GetRecipes() => await GetAll();

        public async Task<IServiceResponse<Recipe>> GetRecipe(Guid recipeId) => await Get(recipeId);

        public async Task<IServiceResponse<Recipe>> DeleteRecipe(Guid recipeId) => await Delete(recipeId);

        public async Task<IServiceResponse<Recipe>> CreateRecipe(PostRecipeRequest request) => await Create(request);

        public async Task<IServiceResponse<Recipe>> UpdateRecipe(Guid recipeId, UpdateRecipeRequest request) => await Update(recipeId, request);
    }
}