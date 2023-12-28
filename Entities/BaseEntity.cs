namespace MomsKitchen.Entities;

public abstract class BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime UpdatedBy { get; set; }
    public DateTime DeletedAt { get; set; }
    public DateTime DeletedBy { get; set; }
    public bool IsActive { get; set; }
}