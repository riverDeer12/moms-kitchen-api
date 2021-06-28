using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MomsKitchen.API.Services;
using MomsKitchen.DATA.DTO.Categories;

namespace MomsKitchen.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _categoriesService.GetCategories();

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpGet]
        [Route("{categoryId}")]
        public async Task<IActionResult> Get([FromRoute] string categoryId)
        {
            var response = await _categoriesService.GetCategory(categoryId);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostCategoryRequest request)
        {
            var response = await _categoriesService.CreateCategory(request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpPut]
        [Route("{categoryId}")]
        public async Task<IActionResult> Put([FromRoute] string categoryId, UpdateCategoryRequest request)
        {
            var response = await _categoriesService.UpdateCategory(categoryId, request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{categoryId}")]
        public async Task<IActionResult> Delete([FromRoute] string categoryId)
        {
            var response = await _categoriesService.DeleteCategory(categoryId);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }
    }
}