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

        public async Task<IServiceResponse<Recipe>> GetRecipe(string categoryId) => await Get(categoryId);

        public async Task<IServiceResponse<Recipe>> DeleteRecipe(string categoryId) => await Delete(categoryId);

        public async Task<IServiceResponse<Recipe>> CreateRecipe(PostRecipeRequest request) => await Create(request);

        public async Task<IServiceResponse<Recipe>> UpdateRecipe(string categoryId, UpdateRecipeRequest request) => await Update(categoryId, request);
    }
}