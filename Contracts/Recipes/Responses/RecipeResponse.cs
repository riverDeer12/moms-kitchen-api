using MomsKitchen.Entities;

namespace MomsKitchen.Contracts.Recipes.Responses;

public class RecipeResponse {
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public ApplicationUser CreatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public ApplicationUser UpdatedBy { get; set; }
}