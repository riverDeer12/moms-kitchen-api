using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using MomsKitchen.Constants;
using MomsKitchen.Contracts.Recipes.Responses;
using MomsKitchen.Exceptions;

namespace MomsKitchen.Endpoints.Recipes;

public class GetRecipeEndpoint : EndpointWithoutRequest<RecipeResponse>
{
    private readonly MomsKitchenContext _db;

    public GetRecipeEndpoint(MomsKitchenContext db)
    {
        _db = db;
    }
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("recipes/{id}");
        AllowAnonymous();
        Options(x => x.WithTags("Recipes"));
    }

    public override async Task<RecipeResponse> HandleAsync(CancellationToken cancellationToken)
    {
        var routeId = Route<Guid?>("id");

        if (routeId is null) throw new NotFoundException(ValidationMessages.NotFound);

        var recipe = await _db.Recipes.FirstOrDefaultAsync(x => x.RecipeId == routeId,
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