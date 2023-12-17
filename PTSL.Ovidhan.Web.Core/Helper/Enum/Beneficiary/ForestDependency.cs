using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.Beneficiary
{
    public enum ForestDependency
    {
        [Display(Name = "90-100%")]
        NinetyToHundred = 1,

        [Display(Name = "80-90%")]
        EightyToNinety = 2,

        [Display(Name = "70-80%")]
        SeventyToEighty = 3,

        [Display(Name = "60-70%")]
        SixtyToSeventy = 4,

        [Display(Name = "50-60%")]
        FiftyToSixty = 5,

        [Display(Name = "Less than 50%")]
        LessThenFifty = 6
    }
}