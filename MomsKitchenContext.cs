using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MomsKitchen.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MomsKitchen.Constants;
using MomsKitchen.Exceptions;

namespace MomsKitchen;

public class MomsKitchenContext : IdentityDbContext
{
    /**
     * Seeding constant values.
     */
    private const string SuperAdminId = "cd75a482-cac0-45f2-9c20-bae54f363742";

    private const string SuperAdminRoleId = "69a4116d-b1bd-4f0b-b6a7-a13bb5eb639f";
    private const string AdminRoleId = "a1897ddf-24d5-43cb-af30-1e8425003eae";
    private const string UserRoleId = "2e275270-0e64-4926-905e-70a2fab92006";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public MomsKitchenContext(DbContextOptions<MomsKitchenContext> options, IHttpContextAccessor httpContextAccessor) :
        base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /**
     * DB sets.
     */
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        # region renaming identity tables

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

        # endregion

        # region seeding

        modelBuilder.Entity<IdentityRole>().HasData(Roles);

        modelBuilder.Entity<ApplicationUser>().HasData(GenerateSuperAdmin());

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(UserRoles);

        # endregion
    }

    /**
     * Prepare default Roles
     * for seeding the data.
     */
    private static List<IdentityRole> Roles => new()
    {
        new IdentityRole { Id = SuperAdminRoleId, Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
        new IdentityRole { Id = AdminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
        new IdentityRole { Id = UserRoleId, Name = "User", NormalizedName = "USER" }
    };

    /**
     * Prepare default super admin user
     * configuration. Connecting user with
     * super admin role.
     */
    private static List<IdentityUserRole<string>> UserRoles => new()
    {
        new IdentityUserRole<string> { UserId = SuperAdminId, RoleId = SuperAdminRoleId }
    };
    
    /**
     * Helper method for getting
     * logged user id.
     */
    private Guid GetLoggedUserId()
    {
        if (_httpContextAccessor.HttpContext == null) return Guid.Empty;

        var loggedUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

        if (loggedUserId == null)
            throw new BadRequestException(ValidationMessages.UnauthorizedAction);

        return Guid.Parse(loggedUserId);
    }

    /**
     * Method that overrides
     * save changes process depending
     * on action and logged user. Automates
     * actions that would need to be written on
     * every record save/update.
     */
    public override async Task<int> SaveChangesAsync(bool softDelete, CancellationToken cancellationToken)
    {
        var issuerId = GetLoggedUserId();

        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    ((BaseEntity)entityEntry.Entity).InitializeCreate(issuerId);
                    break;
                case EntityState.Modified:
                    ((BaseEntity)entityEntry.Entity).InitializeUpdate(issuerId);
                    break;
                case EntityState.Deleted:
                    ((BaseEntity)entityEntry.Entity).InitializeDelete(issuerId);
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return await base.SaveChangesAsync(true, cancellationToken).ConfigureAwait(false);
    }

    /**
     * Helper method that
     * generates new user with super
     * admin role.
     */
    private static ApplicationUser GenerateSuperAdmin()
    {
        var superAdmin = new ApplicationUser
        {
            Id = SuperAdminId,
            FirstName = "Super",
            LastName = "Admin",
            Address = "Bartola Kašića 10",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ActivityUpdatedAt = DateTime.UtcNow,
            UpdatedBy = Guid.Parse(SuperAdminId),
            CreatedBy = Guid.Parse(SuperAdminId),
            ActivityUpdatedBy = Guid.Parse(SuperAdminId),
            IsActive = true,
            TwoFactorEnabled = true,
            Email = "superadmin@gmail.com",
            PhoneNumber = "+385915007122",
            UserName = "superadmin",
            NormalizedUserName = "SUPERADMIN",
            NormalizedEmail = "SUPERADMIN@GMAIL.COM"
        };

        var password = new PasswordHasher<ApplicationUser>();

        var hashed = password.HashPassword(superAdmin, "superAdmin123#");

        superAdmin.PasswordHash = hashed;

        return superAdmin;
    }
}