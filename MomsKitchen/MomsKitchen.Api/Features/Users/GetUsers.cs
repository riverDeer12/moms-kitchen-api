using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using MomsKitchen.Api.Database;

namespace MomsKitchen.Api.Features.Users;

public class GetUsers : EndpointWithoutRequest<List<UserResponse>>
{
    private readonly MomsKitchenContext _db;

    public override void Configure()
    {
        Get("/api/users");
        AllowAnonymous();
        Options(x => x.WithTags("Users"));
    }

    public GetUsers(MomsKitchenContext db)
    {
        _db = db;
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        await SendAsync(await GetApplicationUsers(), cancellation: cancellationToken);
    }
    
    private async Task<List<UserResponse>> GetApplicationUsers()
    {
        Console.WriteLine(User.Identity?.Name);
        
        var users = await _db.Users.ToListAsync();

        if (!users.Any()) return new List<UserResponse>();

        return users.Select(user => new UserResponse()
        {
            Id = user.Id,
            Username = user.UserName
        }).ToList();
    }

}

public class UserResponse
{
    public string Id { get; set; }

    public string Username { get; set; }
}