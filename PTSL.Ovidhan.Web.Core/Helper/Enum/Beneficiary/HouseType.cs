using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.Beneficiary
{
    public enum HouseType
    {
        [Display(Name = "Thatched (খড়ের ঘর)")]
        Thatched = 1,

        [Display(Name = "Mud house (মাটির ঘর)")]
        Mudhouse = 2,

        [Display(Name = "Tin Shed (টিন শেড)")]
        TinShed = 3,

        [Display(Name = "Semi-Pacca (আধা-পাকা)")]
        SemiPacca = 4,

        [Display(Name = "Brick built (পাকা বিল্ডিং)")]
        BrickBuilt = 5,
    }
}