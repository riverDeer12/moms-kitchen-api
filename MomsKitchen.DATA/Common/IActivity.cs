using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomsKitchen.DATA.Common
{
    public interface IActivity
    {
        public bool IsActive { get; set; }
        public Guid? ActivityUpdatedBy { get; set; }
        public DateTime ActivityUpdatedAt { get; set; }
    }
}
