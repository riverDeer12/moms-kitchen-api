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
}