namespace MomsKitchen.Entities;

public abstract class BaseEntity
{
    public DateTime CreatedAt { get; private set; }

    private Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; private set; }

    private Guid UpdatedBy { get; set; }

    private bool IsDeleted { get; set; }

    private DateTime DeletedAt { get; set; }

    private Guid DeletedBy { get; set; }

    public bool IsActive { get; set; }

    public void InitializeCreate(Guid loggedUserId)
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = loggedUserId;
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = loggedUserId;
    }

    public void InitializeUpdate(Guid loggedUserId)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = loggedUserId;
    }

    public void InitializeDelete(Guid loggedUserId, bool softDelete = true)
    {
        DeletedAt = DateTime.UtcNow;
        DeletedBy = loggedUserId;
        IsDeleted = true;
    }
}