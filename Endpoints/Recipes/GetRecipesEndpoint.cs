using FastEndpoints;

namespace MomsKitchen.Endpoints.Recipes;

public class GetRecipesEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/recipes");
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