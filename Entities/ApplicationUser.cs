using Microsoft.AspNetCore.Identity;

namespace MomsKitchen.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Address { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public Guid? ActivityUpdatedBy { get; set; }

    public DateTime ActivityUpdatedAt { get; set; }
}