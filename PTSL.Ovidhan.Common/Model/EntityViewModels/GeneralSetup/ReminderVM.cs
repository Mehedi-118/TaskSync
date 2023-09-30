using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup
{
    public class ReminderVM : BaseModel
    {
        public long? TodoId { get; set; }
        public string? UserId { get; set; }

        public DateTime? RemindAt { get; set; }
    }
}
