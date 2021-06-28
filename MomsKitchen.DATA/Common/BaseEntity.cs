using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomsKitchen.DATA.Common
{
    public abstract class BaseEntity : ITimestamps, ISoftDelete, IActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Guid? DeletedBy { get; set; }

        public bool IsActive { get; set; }

        public Guid? ActivityUpdatedBy { get; set; }

        public DateTime ActivityUpdatedAt { get; set; }
    }
}
