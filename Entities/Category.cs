using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomsKitchen.Entities;

public class Category : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Recipe> Recipes { get; set; }
}