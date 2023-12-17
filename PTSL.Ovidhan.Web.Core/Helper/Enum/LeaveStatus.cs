using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum;

public enum LeaveStatus
{
    //Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
    [Display(Name = "Pending")] Pending = 1,
    [Display(Name = "Cancelled")] Cancelled = 2,
    [Display(Name = "Approved")] Approved = 3
}
