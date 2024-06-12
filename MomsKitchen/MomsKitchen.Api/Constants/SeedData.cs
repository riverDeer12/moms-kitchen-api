using Microsoft.AspNetCore.Identity;

namespace MomsKitchen.Api.Constants;

public abstract class SeedData
{
    private const string SuperAdminId = "5604e898-cd94-476b-8b86-9aa3a87cc9bb";
    private const string SuperAdminRoleId = "69a4116d-b1bd-4f0b-b6a7-a13bb5eb639f";

    public static IdentityUser GetSuperAdminData()
    {
        var superAdmin = new IdentityUser
        {
            Id = SuperAdminId,
            TwoFactorEnabled = true,
            Email = "superadmin@gmail.com",
            PhoneNumber = "+385915007122",
            UserName = "superadmin",
            EmailConfirmed = true,
            NormalizedUserName = "SUPERADMIN",
            NormalizedEmail = "SUPERADMIN@GMAIL.COM"
        };

        var password = new PasswordHasher<IdentityUser>();

        var hashed = password.HashPassword(superAdmin, "superAdmin123#");

        superAdmin.PasswordHash = hashed;

        return superAdmin;
    }

    public static readonly IdentityRole SuperAdminRole = new()
    {
        Id = SuperAdminRoleId,
        Name = "SuperAdmin",
        NormalizedName = "SUPERADMIN"
    };

    public static readonly IdentityUserRole<string> SuperAdminRelation = new()
    {
        UserId = SuperAdminId,
        RoleId = SuperAdminRoleId
    };
}