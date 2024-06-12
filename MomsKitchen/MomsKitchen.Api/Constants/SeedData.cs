using Microsoft.AspNetCore.Identity;
using MomsKitchen.Api.Database.Entities;

namespace MomsKitchen.Api.Constants;

public static class SeedData
{
    public static readonly User SuperAdminData = new()
    {
        Id = "cd75a482-cac0-45f2-9c20-bae54f363742",
        TwoFactorEnabled = true,
        IsActive = true,
        Email = "superadmin@gmail.com",
        PhoneNumber = "+385915007122",
        UserName = "superadmin",
        EmailConfirmed = true,
        NormalizedUserName = "SUPERADMIN",
        NormalizedEmail = "SUPERADMIN@GMAIL.COM"
    };

    public static readonly Role SuperAdminRole = new()
    {
        Id = "69a4116d-b1bd-4f0b-b6a7-a13bb5eb639f",
        IsActive = true,
        Name = "SuperAdmin",
        CreatedBy = Guid.Parse("cd75a482-cac0-45f2-9c20-bae54f363742"),
        CreatedAt = DateTime.UtcNow,
        UpdatedBy = Guid.Parse("cd75a482-cac0-45f2-9c20-bae54f363742"),
        UpdatedAt = DateTime.UtcNow,
        NormalizedName = "SUPERADMIN"
    };

    public static readonly IdentityUserRole<string> SuperAdminRelation = new()
    {
        UserId = "cd75a482-cac0-45f2-9c20-bae54f363742",
        RoleId = "69a4116d-b1bd-4f0b-b6a7-a13bb5eb639f"
    };
}