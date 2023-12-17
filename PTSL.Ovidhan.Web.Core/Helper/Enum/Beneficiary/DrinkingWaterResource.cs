using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.Beneficiary
{
    public enum DrinkingWaterResource
    {
        [Display(Name = "River/Stream")]
        RiverOrStream = 1,

        [Display(Name = "Pond")]
        Pond = 2,

        [Display(Name = "Community tubewell")]
        CommunityTubeWell = 3,

        [Display(Name = "Own tubewell")]
        OwnTubeWell = 4,

        [Display(Name = "Rain water")]
        RainWater = 5
    }
}