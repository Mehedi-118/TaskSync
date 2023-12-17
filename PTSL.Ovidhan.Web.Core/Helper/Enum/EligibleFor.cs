using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum;

public enum EligibleFor
{
    //Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
    [Display(Name = "Male")] Male = 1,
    [Display(Name = "Female")] Female = 2,
    [Display(Name = "Male, Female (Both)")] Both = 3
}
