using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.Beneficiary
{
    public enum FoodCondition
    {
        [Display(Name = "Usually food deficit (নিত্য খাদ্য ঘাটতি থাকে)")]
        UsuallyFoodDeficit = 1,

        [Display(Name = "Occasionally deficit (কখনো কখনো খাদ্য ঘাটতি থাকে)")]
        OccasionallyDeficit = 2,

        [Display(Name = "Break even (মোটামুটি চলে যায়)")]
        BreakEven = 3,

        [Display(Name = "Surplus (উদ্বত্ব থাকে)")]
        Surplus = 4,
    }
}