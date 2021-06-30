using AutoMapper;
using MomsKitchen.DATA.DTO.Categories;
using MomsKitchen.DATA.DTO.Recipes;
using MomsKitchen.DATA.Entities;

namespace MomsKitchen.API.Configuration
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            /// <summary>
            /// Recipe mappers.
            /// </summary>
            /// <typeparam name="PostRecipeRequest"></typeparam>
            /// <typeparam name="UpdateRecipeRequest"></typeparam>
            /// <typeparam name="Recipe"></typeparam>
            /// <returns></returns>
            CreateMap<PostRecipeRequest, Recipe>();
            CreateMap<UpdateRecipeRequest, Recipe>();

            /// <summary>
            /// Category mappers.
            /// </summary>
            /// <typeparam name="PostCategoryRequest"></typeparam>
            /// <typeparam name="UpdateCategoryRequest"></typeparam>
            /// <typeparam name="Category"></typeparam>
            /// <returns></returns>
            CreateMap<PostCategoryRequest, Category>();
            CreateMap<UpdateCategoryRequest, Category>();
        }
    }
}
