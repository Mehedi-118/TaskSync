using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.ForestManagement;

public enum ExecutiveType
{
    [Display(Name = "Representative From FPCC")]
    RepresentativeFromFpcc = 1,
    [Display(Name = "Representative From VCSC")]
    RepresentativeFromVcsc = 2,
    [Display(Name = "Representative From SAC")]
    RepresentativeFromSac = 3,
    [Display(Name = "Representative From FAC")]
    RepresentativeFromFac = 4,
    [Display(Name = "Representative From PC")]
    RepresentativeFromPc = 5,
    Chairman = 6,
    [Display(Name = "Member Secretary")]
    MemberSecretary = 7,
    [Display(Name = "Representative From Social Forestry")]
    RepresentativeFromSocialForestry = 8,
    [Display(Name = "Representative From PA")]
    RepresentativeFromPa = 9
}