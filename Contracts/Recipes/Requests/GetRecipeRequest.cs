using FastEndpoints;

namespace MomsKitchen.Contracts.Recipes.Requests;

public class GetRecipeRequest
{
    public string RecipeId { get; set; }
}