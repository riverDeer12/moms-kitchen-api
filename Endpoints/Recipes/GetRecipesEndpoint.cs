using FastEndpoints;

namespace MomsKitchen.Endpoints.Recipes;

public class GetRecipesEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("recipes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct) =>
        await SendAsync(new
        {
            message = "Hello"
        }, cancellation: ct);
}