using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.Beneficiary;

public enum SkillLevel
{
    [Display(Name = "Functional (running business) (কার্যকরী (চলমান ব্যবসা))")]
    Functional = 1,

    [Display(Name = "Can work independently (investment required) (স্বাধীনভাবে কাজ করতে পারে (বিনিয়োগ প্রয়োজন))")]
    CanWorkIndependently = 2,

    [Display(Name = "Working under supervision (তত্ত্বাবধানে কাজ করছে)")]
    WorkingUnderSupervision = 3,

    [Display(Name = "Received skill training, but not applied (দক্ষতা প্রশিক্ষণ পেয়েছেন, কিন্তু প্রয়োগ করা হয়নি)")]
    ReceivedSkillTrainingButNotApplied = 4,

    [Display(Name = "No skill (দক্ষতা নেই)")]
    NoSkill = 5
}