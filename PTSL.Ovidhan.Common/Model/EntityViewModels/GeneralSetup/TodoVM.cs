using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup
{
    public class TodoVM : BaseModel
    {
        public string TitleEn { get; set; } = string.Empty;
        public string TitleBn { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionBn { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime DueTime { get; set; }
        public PriorityEnum Priority { get; set; }
        public bool HasReminder { get; set; } = false;
        public DateTime? RemindAt { get; set; }
        public List<ReminderVM>? Reminders { get; set; }
        public long? CategoryId { get; set; }
        public CategoryVM? CategoryVM { get; set; }

        public string? UserId { get; set; }
    }
}
