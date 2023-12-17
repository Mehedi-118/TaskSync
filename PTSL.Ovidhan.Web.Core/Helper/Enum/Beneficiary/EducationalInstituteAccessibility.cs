using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.Beneficiary
{
    public enum EducationalInstituteAccessibility
    {
        [Display(Name = "Good (ভালো)")]
        Good = 1,

        [Display(Name = "Moderate (মধ্যম)")]
        Moderate = 2,

        [Display(Name = "Bad (খারাপ)")]
        Bad = 3
    }
}