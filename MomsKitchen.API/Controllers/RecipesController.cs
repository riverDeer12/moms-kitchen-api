using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MomsKitchen.API.Services;
using MomsKitchen.DATA.DTO.Recipes;

namespace MomsKitchen.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesService _recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            _recipesService = recipesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _recipesService.GetRecipes());

        [HttpGet]
        [Route("{recipeId}")]
        public async Task<IActionResult> Get([FromRoute] Guid recipeId)
            => Ok(await _recipesService.GetRecipe(recipeId));

        [HttpPost]
        public async Task<IActionResult> Post(RecipeRequest request)
            => Ok(await _recipesService.CreateRecipe(request));

        [HttpPut]
        [Route("{recipeId}")]
        public async Task<IActionResult> Put([FromRoute] Guid recipeId, RecipeRequest request)
            => Ok(await _recipesService.UpdateRecipe(recipeId, request));

        [HttpDelete]
        [Route("{recipeId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid recipeId)
            => Ok(await _recipesService.DeleteRecipe(recipeId));

    }
}
