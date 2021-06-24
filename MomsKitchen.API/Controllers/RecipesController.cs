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
        {
            var response = await _recipesService.GetRecipes();

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpGet]
        [Route("{recipeId}")]
        public async Task<IActionResult> Get([FromRoute] string recipeId)
        {
            var response = await _recipesService.GetRecipe(recipeId);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostRecipeRequest request)
        {
            var response = await _recipesService.CreateRecipe(request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpPut]
        [Route("{recipeId}")]
        public async Task<IActionResult> Put([FromRoute] string recipeId, UpdateRecipeRequest request)
        {
            var response = await _recipesService.UpdateRecipe(recipeId, request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{recipeId}")]
        public async Task<IActionResult> Delete([FromRoute] string recipeId)
        {
            var response = await _recipesService.DeleteRecipe(recipeId);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }
    }
}
