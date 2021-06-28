namespace MomsKitchen.DATA.DTO.Recipes
{
    public class UpdateRecipeRequest
    {
        public bool IsActive { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}