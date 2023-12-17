using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.Beneficiary;

public enum SatisfactionLevel
{
    [Display(Name = "Highly Satisfied (খুবই সন্তুষ্ট/কার্যকর)")]
    HighlySatisfied = 1,

    [Display(Name = "Satisfied (সন্তুষ্ট/কার্যকর)")]
    Satisfied = 2,

    [Display(Name = "Moderately Satisfied (মোটামুটি সন্তুষ্ট/কার্যকর)")]
    ModeratelySatisfied = 3,

    [Display(Name = "Moderately Unsatisfied (মোটামুটি অসন্তুষ্ট/অকার্যকর)")]
    ModeratelyUnsatisfied = 4,

    [Display(Name = "Unstatisfied (অসন্তুষ্ট/ অকার্যকর)")]
    Unsatisfied = 5,

    [Display(Name = "Highly Unsatisfied (খুবই অসন্তুষ্ট/অকার্যকর)")]
    HighlyUnsatisfied = 6,

    [Display(Name = "Not Relevant (প্রযোজ্য নয়)")]
    NotRelevant = 7
}