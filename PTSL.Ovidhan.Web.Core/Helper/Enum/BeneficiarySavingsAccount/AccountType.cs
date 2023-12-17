using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.BeneficiarySavingsAccount
{
    public enum AccountType
    {
        [Display(Name = "Monthly")]
        Monthly = 1,

        [Display(Name = "Weekly")]
        Weekly = 2,

        [Display(Name = "Half Monthly")]
        HalfMonthly = 3,
    }
}
