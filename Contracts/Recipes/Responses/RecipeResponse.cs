using MomsKitchen.Entities;

namespace MomsKitchen.Contracts.Recipes.Responses;

public class RecipeResponse {
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}