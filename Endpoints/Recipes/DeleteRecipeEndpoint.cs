using FastEndpoints;

namespace MomsKitchen.Endpoints.Recipes;

public class DeleteRecipeEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Verbs(Http.DELETE);
        Routes("api/recipes/{id}");
        AllowAnonymous();
        Options(x => x.WithTags("Recipes"));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        await SendAsync(new
        {
            message = "Hello"
        }, cancellation: cancellationToken);
    }
}