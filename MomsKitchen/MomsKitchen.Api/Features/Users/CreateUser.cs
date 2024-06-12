using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using MomsKitchen.Api.Constants;

namespace MomsKitchen.Api.Features.Users;

public sealed class CreateUser : Endpoint<CreateUserRequest, CreateUserResponse>
{
    private readonly UserManager<IdentityUser> _userManager;

    public CreateUser(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public override void Configure()
    {
        Post("api/users");
        AllowAnonymous();
    }

    public override async Task<CreateUserResponse> HandleAsync(CreateUserRequest request, CancellationToken c)
    {
        var user = new IdentityUser
        { 
            UserName = request.Username,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new BadHttpRequestException(ValidationMessages.NotValid);

        return new CreateUserResponse(user.Id, user.UserName);
    }
}

public abstract record CreateUserRequest(
    string Username, 
    string Password,
    string Email);

public class CreateUserResponse
{
    public CreateUserResponse(string id, string username)
    {
        Id = id;
        Username = username;
    }

    private string Id { get; set; }
    private string Username { get; set; }
}