using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using MomsKitchen.Api.Constants;
using MomsKitchen.Api.Database.Entities;

namespace MomsKitchen.Api.Features.Users;

public sealed class CreateUser : Endpoint<CreateUserRequest, CreateUserResponse>
{
    private readonly UserManager<User> _userManager;

    public CreateUser(UserManager<User> userManager)
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
        var user = new User
        {
            IsActive = false,
            UserName = request.Username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = Guid.Parse(User.Identity?.Name!),
            UpdatedBy = Guid.Parse(User.Identity?.Name!)
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new BadHttpRequestException(ValidationMessages.NotValid);

        return new CreateUserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName
        };
    }
}

public abstract record CreateUserRequest(
    string FirstName, 
    string LastName, 
    string Username, 
    string Password,
    string Email);

public class CreateUserResponse
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}