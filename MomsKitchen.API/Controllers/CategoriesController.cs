using System;
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
            => Ok(await _categoriesService.GetCategories());

        [HttpGet]
        [Route("{categoryId}")]
        public async Task<IActionResult> Get([FromRoute] Guid categoryId) 
            => Ok(await _categoriesService.GetCategory(categoryId));

        [HttpPost]
        public async Task<IActionResult> Post(CategoryRequest request) 
            => Ok(await _categoriesService.CreateCategory(request));

        [HttpPut]
        [Route("{categoryId}")]
        public async Task<IActionResult> Put([FromRoute] Guid categoryId, CategoryRequest request) 
            => Ok(await _categoriesService.UpdateCategory(categoryId, request));

        [HttpDelete]
        [Route("{categoryId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid categoryId)
            => Ok(await _categoriesService.DeleteCategory(categoryId));
    }
}