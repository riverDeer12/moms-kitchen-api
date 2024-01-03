using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MomsKitchen.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MomsKitchen;

public class MomsKitchenContext : IdentityDbContext
{
    public MomsKitchenContext(DbContextOptions<MomsKitchenContext> options) :
        base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "Users");
                entity.Property(p => p.Id).HasColumnName("UserId");
            });

        modelBuilder
            .Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
                entity.Property(p => p.Id).HasColumnName("RoleId");
            });

        modelBuilder
            .Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });

        modelBuilder
            .Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
                entity.Property(p => p.Id).HasColumnName("UserClaimId");
            });

        modelBuilder
            .Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });

        modelBuilder
            .Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
                entity.Property(p => p.Id).HasColumnName("RoleClaimId");
            });

        modelBuilder
            .Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });
    }
}