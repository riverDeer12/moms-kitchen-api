using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MomsKitchen.Constants;
using MomsKitchen.Exceptions;

namespace MomsKitchen.Entities;

public class Recipe : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid RecipeId { get; private set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Category> Categories { get; set; }

    public void AddCategories(MomsKitchenContext db, List<Guid> categoryIds)
    {
        foreach (var categoryId in categoryIds)
        {
            var category = db.Categories.FirstOrDefault(x => x.CategoryId == categoryId);

            if (category is null) 
                throw new NotFoundException(ValidationMessages.NotFound);
            
            Categories.Add(category);
        }
    }
}