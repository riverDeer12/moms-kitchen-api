using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MomsKitchen.Api.Constants;

namespace MomsKitchen.Api.Database;

public class MomsKitchenContext : IdentityDbContext
{
    public MomsKitchenContext(DbContextOptions<MomsKitchenContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityUser>(entity =>
        {
            entity.ToTable(name: "Users");
            entity.Property(p => p.Id).HasColumnName("UserId");
            entity.HasData(SeedData.GetSuperAdminData());
        });

        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "Roles");
            entity.HasData(SeedData.SuperAdminRole);
        });

        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
            entity.HasKey(key => new { key.UserId, key.RoleId });
            entity.HasData(SeedData.SuperAdminRelation);
        });

        builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });

        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
            entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });
        });

        builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });

        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
            entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });
        });
    }
}