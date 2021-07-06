using MomsKitchen.DATA.Common;
using System.Collections.Generic;

namespace MomsKitchen.DATA.Entities
{
    public class ComplexityLevel : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int ComplexityWeight { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
