using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PTSL.Ovidhan.Common.Enum
{
    public enum Days
    {
        //Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
        [Display(Name = "Saturday")] Saturday = 1,
        [Display(Name = "Sunday")] Sunday = 2,
        [Display(Name = "Monday")] Monday = 3,
        [Display(Name = "Tuesday")] Tuesday = 4,
        [Display(Name = "Wednesday")] Wednesday = 5,
        [Display(Name = "Thursday")] Thursday = 6,
        [Display(Name = "Friday")] Friday = 7
    }
}
