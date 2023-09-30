using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PTSL.Ovidhan.Common.Enum
{
    public enum RecruitmentType
    {
        [Display(Name = "Permanent")]
        Permanent = 1,
        [Display(Name = "Temporary")]
        Temporary = 2,
        [Display(Name = "Others")]
        Others = 2
    }
}
