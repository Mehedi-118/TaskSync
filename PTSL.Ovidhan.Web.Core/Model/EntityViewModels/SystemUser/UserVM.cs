using System.ComponentModel.DataAnnotations;

using PTSL.Ovidhan.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.Ovidhan.Web.Core.Model.GeneralSetup;

namespace PTSL.Ovidhan.Web.Core.Model
{
    public class UserVM : BaseModel
    {
        public string? FullName { get; set; }
        public string? RoleName { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? ImageUrl { get; set; }
        public string? UserPhone { get; set; }
        public string? UserGroup { get; set; }
        public bool UserStatus { get; set; }
        public long PmsGroupID { get; set; }
        public long? GroupID { get; set; }
        public virtual UserGroupVM? Group { get; set; }
        public virtual PmsGroupVM? PmsGroup { get; set; }
    }

    public class LoginVM
    {
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
    }

    public class LoginResultVM
    {
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? Token { get; set; }
        public string? RoleName { get; set; }
    }
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Username is Required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Fullname is Required")]
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string ConfirmPassword { get; set; }
    }


}
