namespace MomsKitchen.Entities;

public class Recipe : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Category> Categories { get; set; }
}