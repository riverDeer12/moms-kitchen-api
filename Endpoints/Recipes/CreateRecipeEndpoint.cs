using FastEndpoints;
using MomsKitchen.Contracts.Recipes.Requests;
using MomsKitchen.Contracts.Recipes.Responses;

namespace MomsKitchen.Endpoints.Recipes;

public class CreateRecipeEndpoint : Endpoint<CreateRecipeRequest, RecipeResponse>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/recipes");
        AllowAnonymous();
        Options(x => x.WithTags("Recipes"));
    }
    
    public override async Task HandleAsync(CreateRecipeRequest request, CancellationToken ct)
    {
        await base.HandleAsync(request, ct);
    }
}