using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.Beneficiary
{
    public enum SanitationFacilities
    {
        [Display(Name = "No sanitary latrine (পয়ঃনিস্কাশনের কোন ব্যবস্থা নাই)")]
        NoSanitaryLatrine = 1,

        [Display(Name = "Kacha (কাঁচা)")]
        Kacha = 2,

        [Display(Name = "Community facility (গণ শৌচাগার)")]
        CommunityFacility = 3,

        [Display(Name = "Own sanitary latrine (নিজস্ব সেনিটারি ল্যাট্রিন আছে)")]
        OwnSanitaryLatrine = 4
    }
}