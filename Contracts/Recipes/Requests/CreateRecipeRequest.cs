namespace MomsKitchen.Contracts.Recipes.Requests;

public class CreateRecipeRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Guid> Categories { get; set; }
}