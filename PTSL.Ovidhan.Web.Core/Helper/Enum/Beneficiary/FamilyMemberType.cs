using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.Beneficiary
{
    public enum FamilyMemberType
    {
        [Display(Name = "Male Children (ছেলে শিশু)")]
        MaleChildren = 1,

        [Display(Name = "Girl Children (মেয়ে শিশু)")]
        GirlChildren = 2,

        [Display(Name = "Men (পুরুষ)")]
        Men = 3,

        [Display(Name = "Women (নারী)")]
        Women = 4,

        [Display(Name = "Everyone same (সবাই একইরকম/সমান পায়)")]
        EveryoneSame = 5,
    }
}