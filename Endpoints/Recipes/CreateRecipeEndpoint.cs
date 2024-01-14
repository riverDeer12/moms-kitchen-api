using FastEndpoints;
using MomsKitchen.Contracts.Recipes.Requests;
using MomsKitchen.Contracts.Recipes.Responses;
using MomsKitchen.Entities;

namespace MomsKitchen.Endpoints.Recipes;

public class CreateRecipeEndpoint : Endpoint<CreateRecipeRequest, RecipeResponse>
{
    private readonly MomsKitchenContext _db;

    public CreateRecipeEndpoint(MomsKitchenContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/recipes");
        AllowAnonymous();
        Options(x => x.WithTags("Recipes"));
    }

    public override async Task<RecipeResponse> HandleAsync(CreateRecipeRequest request,
        CancellationToken cancellationToken)
    {
        var recipe = new Recipe
        {
            IsActive = true,
            Name = request.Name,
            Description = request.Description
        };

        recipe.AddCategories(_db, request.Categories);

        _db.Recipes.Add(recipe);

        try
        {
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return new RecipeResponse
        {
            Name = recipe.Name,
            Description = recipe.Description,
            CreatedAt = recipe.CreatedAt,
            UpdatedAt = recipe.UpdatedAt
        };
    }
}