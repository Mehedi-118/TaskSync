using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum;

public enum RecruitPromoEnum
{
    [Display(Name = "By Recruitment")]
    ByRecruitment = 1,
    [Display(Name = "By Promotion")]
    ByPromotion = 2
}
