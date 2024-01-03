using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomsKitchen.Entities;

public class Recipe : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid RecipeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Category> Categories { get; set; }
}