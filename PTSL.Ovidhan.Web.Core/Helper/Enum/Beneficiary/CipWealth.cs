using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.Beneficiary;

public enum CipWealth
{
    [Display(Name = "Extreme Poor")]
    ExtremePoor = 1,

    [Display(Name = "Poor")]
    Poor = 2,

    [Display(Name = "Middle")]
    Middle = 3,

    [Display(Name = "Rich")]
    Rich = 4,
}
