using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MomsKitchen.DATA.Entities;

namespace MomsKitchen.DATA
{
public class Model : IdentityDbContext
    {
        private const string SUPERADMIN_ID = "cd75a482-cac0-45f2-9c20-bae54f363742";
        private const string SUPERADMIN_ROLE_ID = "69a4116d-b1bd-4f0b-b6a7-a13bb5eb639f";
        private const string ADMIN_ROLE_ID = "a1897ddf-24d5-43cb-af30-1e8425003eae";
        private const string USER_ROLE_ID = "2e275270-0e64-4926-905e-70a2fab92006";

        public Model(DbContextOptions<Model> options) :
            base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

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
                .Entity<IdentityUserRole<string>>(entity =>
                {
                    entity.ToTable("UserRoles");
                });

            modelBuilder
                .Entity<IdentityUserClaim<string>>(entity =>
                {
                    entity.ToTable("UserClaims");
                    entity.Property(p => p.Id).HasColumnName("UserClaimId");
                });

            modelBuilder
                .Entity<IdentityUserLogin<string>>(entity =>
                {
                    entity.ToTable("UserLogins");
                });

            modelBuilder
                .Entity<IdentityRoleClaim<string>>(entity =>
                {
                    entity.ToTable("RoleClaims");
                    entity.Property(p => p.Id).HasColumnName("RoleClaimId");
                });

            modelBuilder
                .Entity<IdentityUserToken<string>>(entity =>
                {
                    entity.ToTable("UserTokens");
                });


            modelBuilder.Entity<RecipeCategory>(entity =>
            {
                entity.ToTable("RecipeCategories");
                entity.HasKey(categoryItem => new { categoryItem.RecipeId, categoryItem.CategoryId });
            });

            modelBuilder.Entity<RecipeCategory>()
                .HasOne(categoryItem => categoryItem.Recipe)
                .WithMany(recipe => recipe.Categories)
                .HasForeignKey(categoryItem => categoryItem.RecipeId);

            modelBuilder.Entity<RecipeCategory>()
                .HasOne(categoryItem => categoryItem.Category)
                .WithMany(category => category.Recipes)
                .HasForeignKey(categoryItem => categoryItem.CategoryId);


            modelBuilder.Entity<IdentityRole>().HasData(Roles);

            modelBuilder.Entity<ApplicationUser>().HasData(GenerateSuperAdmin());

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(UserRoles);
        }

        private static List<IdentityRole> Roles => new()
        {
            new IdentityRole { Id = SUPERADMIN_ROLE_ID, Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
            new IdentityRole { Id = ADMIN_ROLE_ID, Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = USER_ROLE_ID, Name = "User", NormalizedName = "USER" }
        };

        private static List<IdentityUserRole<string>> UserRoles => new()
        {
            new IdentityUserRole<string>{ UserId = SUPERADMIN_ID, RoleId = SUPERADMIN_ROLE_ID }
        };

        private static ApplicationUser GenerateSuperAdmin()
        {
            var superAdmin = new ApplicationUser
            {
                Id = SUPERADMIN_ID,
                FirstName = "Super",
                LastName = "Admin",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ActivityUpdatedAt = DateTime.Now,
                UpdatedBy = Guid.Parse(SUPERADMIN_ID),
                CreatedBy = Guid.Parse(SUPERADMIN_ID),
                ActivityUpdatedBy = Guid.Parse(SUPERADMIN_ID),
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
}