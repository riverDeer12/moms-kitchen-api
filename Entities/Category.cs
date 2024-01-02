namespace MomsKitchen.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Recipe> Recipes { get; set; }
}