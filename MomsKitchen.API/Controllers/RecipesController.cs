using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MomsKitchen.API.Services;
using MomsKitchen.DATA.DTO.Recipes;

namespace MomsKitchen.API.Controllers
{
    /// <summary>
    /// Main controller for recipes managment
    /// <response code="401">If you are not authorized</response>
    /// <response code="403">If you are not allowed</response>
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesService _recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            _recipesService = recipesService;
        }

        /// <summary>
        /// Gets all recipes        
        /// </summary>
        /// <returns>Service response with list of recipe objects</returns>
        /// <response code="200">If there is created recipes</response>
        /// <response code="204">If there is no created recipes</response>          
        /// <response code="400">If there is problem getting recipes</response>    
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var response = await _recipesService.GetRecipes();

            if (!response.Success) return BadRequest(response);

            if (response.Results.Count == 0) return NoContent();

            return Ok(response);
        }

        /// <summary>
        /// Gets recipe by id           
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns>Service response with recipe object</returns>
        /// <response code="200">If recipe is found</response>
        /// <response code="404">If there is recipe found</response>          
        /// <response code="400">If there is problem getting recipe</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{recipeId}")]
        public async Task<IActionResult> Get([FromRoute] Guid recipeId)
        {
            var response = await _recipesService.GetRecipe(recipeId);

            if (!response.Success) return BadRequest(response);

            if (response.Result == null) return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Creates new recipe
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Service response with created recipe object</returns>
        /// <response code="200">If recipe is successfully created</response>
        /// <response code="400">If there is validation errors</response>    
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(PostRecipeRequest request)
        {
            var response = await _recipesService.CreateRecipe(request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Updates recipe data
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="request"></param>
        /// <returns>Service response with updated recipe object</returns>
        /// <response code="200">If recipe is successfully updated</response>
        /// <response code="400">If there is validation errors</response>    
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("{recipeId}")]
        public async Task<IActionResult> Put([FromRoute] Guid recipeId, UpdateRecipeRequest request)
        {
            var response = await _recipesService.UpdateRecipe(recipeId, request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Archives recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns>Service response with deleted recipe object</returns>
        /// <response code="200">If recipe is successfully deleted</response>
        /// <response code="400">If there is validation errors</response>    
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("{recipeId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid recipeId)
        {
            var response = await _recipesService.DeleteRecipe(recipeId);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }
    }
}
