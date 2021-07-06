using System;
using System.Collections.Generic;
using MomsKitchen.DATA.Common;

namespace MomsKitchen.DATA.Entities
{
    public class Recipe : BaseEntity
    {
        public string Name {get; set;}

        public string Description {get; set;}

        public ComplexityLevel ComplexityLevel { get; set; }

        public ICollection<RecipeCategory> Categories { get; set; }
    }
}