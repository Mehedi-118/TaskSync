using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum;

public enum BloodGroup
{
    [Display(Name = "O+")]
    OPositive,
    [Display(Name = "A+")]
    APositive,
    [Display(Name = "B+")]
    BPositive,
    [Display(Name = "AB+")]
    ABPositive,
    [Display(Name = "AB-")]
    ABNegative,
    [Display(Name = "A-")]
    ANegative,
    [Display(Name = "B-")]
    BNegative,
    [Display(Name = "O-")]
    ONegative

}
