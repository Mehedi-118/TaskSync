using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.Beneficiary
{
    public enum LandClassification
    {
        [Display(Name = "Own Land (as per records) (নিজস্ব জমি)")]
        OwnLand = 1,

        [Display(Name = "Leased land (inward/taken) (লিজকৃত জমি)")]
        LeasedLand = 2,

        [Display(Name = "Share cropper land (বর্গা জমি)")]
        ShareCropperLand = 3,

        [Display(Name = "Supported/Donated land by Govt or Other agencies (সরকার বা অন্য কোন সংস্থার সহায়তায় প্রাপ্ত জমি)")]
        SupportedOrDonatedLandByGovtOrOtherAgencies = 4,

        [Display(Name = "Others (অন্যান্য)")]
        Others = 5
    }
}
