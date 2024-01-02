using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomsKitchen.Entities;

public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime CreatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public DateTime UpdatedBy { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public DateTime DeletedAt { get; set; }
    
    public DateTime DeletedBy { get; set; }
    
    public bool IsActive { get; set; }
}