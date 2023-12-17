using System;
using System.ComponentModel.DataAnnotations;

using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Entity.Tasks;
using PTSL.Ovidhan.Common.Model.EntityViewModels;

namespace PTSL.Ovidhan.Common.Model;

public class UserVM                         // In Case of Update User data, make sure updated property is assigned in UserBusiness Update method()
{
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Occupation { get; set; }
    public string? Address { get; set; }
    public bool EmailConfirmed { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public List<Todo>? Tasks { get; set; } = new List<Todo>();
    public List<Category>? Categories { get; set; } = new List<Category>();

}

public class LoginVM
{
    [Required(ErrorMessage = "Username/Email is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string UserPassword { get; set; }

    [Required(ErrorMessage = "Remember Me is required")]
    public bool RememberMe { get; set; }
}

public class LoginResultVM
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? FullName { get; set; }
    public string? OTP { get; set; }
    public bool? IsOtpVerified { get; set; }

}

public class UserDropdownVM
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public long? UserCount { get; set; }

}

public class UserProfilePictureUploadResponseVM
{
    public string? ProfilePictureUrl { get; set; }
    public string? FileName { get; set; }
}

public class UserUpdateVM
{
    public string Id { get; set; }

    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Occupation { get; set; }
    public string? Address { get; set; }
}
public class UserChangePasswordVM
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string NewPassword { get; set; } = string.Empty;
    [Required]
    public string OldPassword { get; set; } = string.Empty;
    [Required]
    public string ConfirmPassword { get; set; } = string.Empty;
}
public class UserCredentialValidationVM
{
    public string? UserName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
}

public class SocialMediaRegistrationVM
{
    public string? UserName { get; set; }
    [Required]
    public string UserEmail { get; set; }
    [Required]
    public string UserPassword { get; set; }
    public string? FullName { get; set; }
    public string? Nid { get; set; }
    public string? MobileNumber { get; set; }


}
public class UserVMSocialMedia
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserPassword { get; set; }
    public string MobileNumber { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? FullName { get; set; }

}
public class OtpverificationVM
{
    public string? UserEmail { get; set; }
    public string? MobileNumber { get; set; }
    [Required]
    public string OTP { get; set; }
    [Required]
    public DateTime Time { get; set; } = DateTime.Now;

}
public class ResendOtpVM
{
    public string? UserEmail { get; set; }
    public string? MobileNumber { get; set; }
}
public class ForgotPasswordVM
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string NewPassword { get; set; } = string.Empty;
    [Required]
    public string ConfirmPassword { get; set; } = string.Empty;
    [Required]
    public string Otp { get; set; } = string.Empty;

}
