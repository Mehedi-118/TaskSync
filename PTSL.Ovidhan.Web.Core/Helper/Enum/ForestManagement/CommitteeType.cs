using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.ForestManagement
{
    public enum CommitteeType
    {
        [Display(Name = "Executive Committee")]
        ExecutiveCommittee = 1,
        [Display(Name = "Sub Executive Committee")]
        SubExecutiveCommittee = 2
    }
}