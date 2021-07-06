using System.Collections.Generic;
using MomsKitchen.DATA.Common;

namespace MomsKitchen.DATA.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public  ICollection<RecipeCategory> Recipes { get; set; }
    }
}
