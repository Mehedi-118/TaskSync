using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum;

public enum RecruitmentType
{
    [Display(Name = "Permanent")]
    Permanent = 1,
    [Display(Name = "Temporary")]
    Temporary = 2,
    [Display(Name = "Others")]
    Others = 2
}
