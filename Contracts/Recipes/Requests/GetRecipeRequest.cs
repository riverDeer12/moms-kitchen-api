using FastEndpoints;

namespace MomsKitchen.Contracts.Recipes.Requests;

public class GetRecipeRequest
{
    [QueryParam]
    public string RecipeId { get; set; }
}