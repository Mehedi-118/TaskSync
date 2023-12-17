using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.Beneficiary
{
    public enum EducationLevel
    {
        [Display(Name = "No education")]
        NoEducation = 1,

        [Display(Name = "Primary Completed")]
        PrimaryCompleted = 2,

        [Display(Name = "Secondary Completed")]
        SecondaryCompleted = 3,

        [Display(Name = "Higher Secondary Completed")]
        HigerSecondaryCompleted = 4,

        [Display(Name = "Graduation and above")]
        GraduationAndAbove = 5,

        [Display(Name = "Sign only")]
        SignOnly = 6
    }
}
