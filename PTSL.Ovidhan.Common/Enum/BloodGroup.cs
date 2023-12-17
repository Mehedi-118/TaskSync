using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PTSL.Ovidhan.Common.Enum
{
    public enum BloodGroup
    {
        [Display(Name = "A Positive")]
        APositive,
        [Display(Name = "A Negative")]
        ANegative,
        [Display(Name = "B Positive")]
        BPositive,
        [Display(Name = "B Negative")]
        BNegative,
        [Display(Name = "AB Positive")]
        ABPositive,
        [Display(Name = "AB Negative")]
        ABNegative,
        [Display(Name = "O Positive")]
        OPositive,
        [Display(Name = "O Negative")]
        ONegative,
        [Display(Name = "N/A")]
        NA

    }
}
