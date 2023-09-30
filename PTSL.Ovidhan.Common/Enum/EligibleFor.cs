using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PTSL.Ovidhan.Common.Enum
{
    public enum EligibleFor
    {
        //Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
        [Display(Name = "Male")] Male = 1,
        [Display(Name = "Female")] Female = 2,
        [Display(Name = "Male, Female (Both)")] Both = 3
    }
}
