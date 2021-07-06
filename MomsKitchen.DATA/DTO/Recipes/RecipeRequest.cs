using System;

namespace MomsKitchen.DATA.DTO.Recipes
{
    public class RecipeRequest
    {
        public bool IsActive { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ComplexityLevelId { get; set; }
    }
}