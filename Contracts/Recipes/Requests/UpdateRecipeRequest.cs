namespace MomsKitchen.Contracts.Recipes.Requests;

public class UpdateRecipeRequest
{
    public bool IsActive { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Guid> Categories { get; set; }
}