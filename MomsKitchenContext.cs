using Microsoft.EntityFrameworkCore;
using MomsKitchen.Entities;

namespace MomsKitchen;

public class MomsKitchenContext : DbContext
{
    public MomsKitchenContext(DbContextOptions<MomsKitchenContext> options) :
        base(options)
    {
    }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Category> Categories { get; set; }
}