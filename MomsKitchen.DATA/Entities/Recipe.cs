using System.Collections.Generic;
using MomsKitchen.DATA.Common;

namespace MomsKitchen.DATA.Entities
{
    public class Recipe : BaseEntity
    {
        public string Name {get; set;}
        public string Description {get; set;}
        public IList<RecipeCategory> Categories { get; set; }
    }
}