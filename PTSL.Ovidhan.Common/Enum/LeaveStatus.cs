using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PTSL.Ovidhan.Common.Enum
{
    public enum LeaveStatus
    {
        //Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
        [Display(Name = "Pending")] Pending = 1,
        [Display(Name = "Cancelled")] Cancelled = 2,
        [Display(Name = "Approved")] Approved = 3
    }
}
