using Microsoft.AspNetCore.Identity;

namespace MomsKitchen.Api.Database.Entities;

public class Role : IdentityRole
{
    public DateTime CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }
    public bool IsActive { get; set; }
}