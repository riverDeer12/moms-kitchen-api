using AutoMapper;
using MomsKitchen.DATA.DTO.ApplicationUsers;
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
            CreateMap<RecipeRequest, Recipe>();
            CreateMap<Recipe, RecipeDetails>();

            /// <summary>
            /// Category mappers.
            /// </summary>
            /// <typeparam name="PostCategoryRequest"></typeparam>
            /// <typeparam name="UpdateCategoryRequest"></typeparam>
            /// <typeparam name="Category"></typeparam>
            /// <returns></returns>
            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, CategoryDetails>();

            /// <summary>
            /// Application user mappers.
            /// </summary>
            /// <typeparam name="PostUserRequest"></typeparam>
            /// <typeparam name="UpdateUserRequest"></typeparam>
            /// <typeparam name="ApplicationUser"></typeparam>
            /// <returns></returns>
            CreateMap<UserRequest, ApplicationUser>();
            CreateMap<ApplicationUser, ApplicationUserDetails>();
        }
    }
}
