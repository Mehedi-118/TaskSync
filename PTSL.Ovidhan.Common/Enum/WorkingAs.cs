using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PTSL.Ovidhan.Common.Enum
{
    public enum WorkingAs
    {
        [Display(Name = "Others")]
        Others = 1,
        [Display(Name = "Current Charge (CC)")]
        Current_Charge_CC = 2,
        [Display(Name = "Attachment")]
        Attachment = 3,
        [Display(Name = "Deputation")]
        Deputation = 4,
        [Display(Name = "Interim with self pay")]
        Interim_with_self_pay = 5,
        [Display(Name = "Lien")]
        Lien = 6,
        [Display(Name = "OSD")]
        OSD = 7

    }
}
