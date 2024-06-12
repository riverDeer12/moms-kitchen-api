using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace MomsKitchen.Api.Features.Authentication;

public class Login : Endpoint<LoginRequest, LoginResponse>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public override void Configure()
    {
        Post("/api/authentication/login");
        AllowAnonymous();
        Options(x => x.WithTags("Authentication"));
    }

    public Login(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
        IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(await RunLogin(request), cancellation: cancellationToken);
    }

    private async Task<LoginResponse> RunLogin(LoginRequest request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Username,
            request.Password, true, false);

        if (!result.Succeeded)
            throw new BadHttpRequestException("Wrong username or password.");


        return new LoginResponse
        {
            AccessToken = await GetAuthToken(request.Username)
        };
    }

    private async Task<string> GetAuthToken(string username)
    {
        var user = await _userManager.FindByNameAsync(username!);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user!.Id!),
            new(ClaimTypes.Email, user!.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var userRoles = await _userManager.GetRolesAsync(user);

        authClaims.AddRange(userRoles
            .Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var token = GenerateJwtToken(authClaims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> authClaims)
    {
        var secretKey = Encoding.UTF8.GetBytes(_configuration["JwtSecretKey"]!);

        var authSigningKey = new SymmetricSecurityKey(secretKey);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(4),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

}

public record LoginRequest(
    string Username, 
    string Password, 
    bool RememberMe);

public class LoginResponse
{
    public string AccessToken { get; set; }
}