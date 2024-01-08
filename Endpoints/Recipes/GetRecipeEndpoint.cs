using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using MomsKitchen.Constants;
using MomsKitchen.Contracts.Recipes.Requests;
using MomsKitchen.Contracts.Recipes.Responses;
using MomsKitchen.Exceptions;

namespace MomsKitchen.Endpoints.Recipes;

public class GetRecipeEndpoint : Endpoint<GetRecipeRequest, RecipeResponse>
{
    private readonly MomsKitchenContext _db;

    public GetRecipeEndpoint(MomsKitchenContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("api/recipes/{recipeId}");
        Options(x => x.WithTags("Recipes"));
        AllowAnonymous();
    }

    public override async Task<RecipeResponse> HandleAsync(GetRecipeRequest request,
        CancellationToken cancellationToken)
    {
        var recipeId = new Guid(request.RecipeId);
        
        var recipe = await _db.Recipes.FirstOrDefaultAsync(x => x.RecipeId == recipeId,
            cancellationToken: cancellationToken);

        if (recipe == null) throw new NotFoundException(ValidationMessages.NotFound);

        return new RecipeResponse
        {
            Name = recipe.Name,
            Description = recipe.Description,
            CreatedAt = recipe.CreatedAt,
            UpdatedAt = recipe.UpdatedAt
        };
    }
}