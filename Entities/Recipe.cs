namespace MomsKitchen.Entities;

public class Recipe : BaseEntity
{
    public Guid RecipeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Category> Categories { get; set; }
}