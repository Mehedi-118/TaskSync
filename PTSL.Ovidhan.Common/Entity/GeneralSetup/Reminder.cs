using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Entity.Tasks;

namespace PTSL.Ovidhan.Common.Entity.GeneralSetup
{
    public class Reminder : BaseEntity
    {
        public long? TodoId { get; set; }
        public string? UserId { get; set; }

        public DateTime? RemindAt { get; set; }
        public Todo? Todo { get; set; }
        public User? User { get; set; }
    }
}
