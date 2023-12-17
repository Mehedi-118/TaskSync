using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.BeneficiarySavingsAccount
{
    public enum WithdrawDfoStatusEnum
    {
        [Display(Name = "Padding")]
        Padding = 1,

        [Display(Name = "Approved")]
        Approved = 2,

        [Display(Name = "Rejected")]
        Rejected = 3,
    }
}
