using System;
using Microsoft.AspNetCore.Identity;
using MomsKitchen.DATA.Common;

namespace MomsKitchen.DATA.Entities
{
    public class ApplicationUser : IdentityUser, ITimestamps, IActivity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public bool IsActive { get; set; }

        public Guid? ActivityUpdatedBy { get; set; }

        public DateTime ActivityUpdatedAt { get; set; }
    }
}
