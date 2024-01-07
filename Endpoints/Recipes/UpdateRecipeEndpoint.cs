using FastEndpoints;
using MomsKitchen.Contracts.Recipes.Requests;
using MomsKitchen.Contracts.Recipes.Responses;

namespace MomsKitchen.Endpoints.Recipes;

public class UpdateRecipeEndpoint : Endpoint<UpdateRecipeRequest, RecipeResponse>
{
    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("api/recipes");
        AllowAnonymous();
        Options(x => x.WithTags("Recipes"));
    }
    
    public override async Task HandleAsync(UpdateRecipeRequest request, CancellationToken ct)
    {
        await base.HandleAsync(request, ct);
    }
}