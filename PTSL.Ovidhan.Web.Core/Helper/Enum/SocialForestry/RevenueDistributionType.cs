using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.SocialForestry;

public enum RevenueDistributionType
{
    [Display(Name = "Forest Department (FD)")]
    ForestDepartment = 1,

    [Display(Name = "Beneficiary")]
    Beneficiary = 2,

    [Display(Name = "TFF")]
    TFF = 3
}
