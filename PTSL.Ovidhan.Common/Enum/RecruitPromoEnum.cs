using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PTSL.Ovidhan.Common.Enum
{
    public enum RecruitPromoEnum
    {
        [Display(Name = "By Recruitment")]
        ByRecruitment = 1,
        [Display(Name = "By Promotion")]
        ByPromotion = 2
    }
}
