using System;

using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup;

namespace PTSL.Ovidhan.Common.Entity.Tasks
{
    public class Todo : BaseEntity
    {
        public string TitleEn { get; set; } = string.Empty;
        public string TitleBn { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionBn { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime DueTime { get; set; }
        public PriorityEnum Priority { get; set; }
        public bool HasReminder { get; set; } = false;
        public List<Reminder> Reminders { get; set; } = new List<Reminder>();

        public string? UserId { get; set; }
        public long? CategoryId { get; set; }
        public Category? Category { get; set; }
        public User? User { get; set; }
    }
}
