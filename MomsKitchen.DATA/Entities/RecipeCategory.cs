using System;

namespace MomsKitchen.DATA.Entities
{
    public class RecipeCategory
    {
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}