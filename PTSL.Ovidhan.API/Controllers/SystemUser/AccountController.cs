using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using MailKit.Net.Smtp;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

using Org.BouncyCastle.Math.EC.Endo;

using PTSL.Ovidhan.Api.Helpers;
using PTSL.Ovidhan.API.Attributes;
using PTSL.Ovidhan.Business.TokenHelper;
using PTSL.Ovidhan.Common.Const;
using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Entity.UserEntitys;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Helper;
using PTSL.Ovidhan.Common.Model;
using PTSL.Ovidhan.Common.Model.EntityViewModels.SystemUser;
using PTSL.Ovidhan.Service.Services.Interface;
using PTSL.Ovidhan.Service.Services.Interface.GeneralSetup;
using PTSL.Ovidhan.Service.Services.Interface.SystemUser;

using static System.Net.WebRequestMethods;

namespace PTSL.Ovidhan.Api.Controllers.SystemUser;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _config;
    private readonly IGenerateJSONWebToken _generateJSONWebToken;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly UserManager<User> _userManager;
    private readonly ICacheService _cache;
    private readonly IMessageService _messageService;
    private readonly ICategoryService _categoryService;


    public AccountController(IUserService userService, IConfiguration config,
                             IGenerateJSONWebToken generateJSONWebToken, IWebHostEnvironment webHostEnvironment,
                             UserManager<User> userManager, ICacheService cache, IMessageService messageService,
                             ICategoryService categoryService)
    {
        _userService = userService;
        _config = config;
        _generateJSONWebToken = generateJSONWebToken;
        _webHostEnvironment = webHostEnvironment;
        _userManager = userManager;
        _cache = cache;
        _messageService = messageService;
        _categoryService = categoryService;
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    [ModelStateValidate]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<LoginResultVM>>> Login([FromBody] LoginVM model)
    {

        (ExecutionState executionState, UserVM? entity, string? refreshToken, string message) = await _userService.Login(model);
        (ExecutionState executionState, LoginResultVM entity, string message) responseResult;

        if (executionState == ExecutionState.Success && entity is not null)
        {
            LoginResultVM loginResult = new()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                AccessToken = _generateJSONWebToken.GetToken(entity, model.RememberMe),
                RefreshToken = refreshToken,
                FullName = entity.FullName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber

            };

            responseResult.entity = loginResult;
            responseResult.message = message;
            responseResult.executionState = executionState;
            WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
            return Ok(apiResponse);
        }
        if (executionState == ExecutionState.Failure)
        {
            LoginResultVM loginResult = new()
            {
                Id = entity?.Id,
                UserName = entity?.UserName,
                Email = entity?.Email
            };

            responseResult.entity = loginResult;
            responseResult.message = message;
            responseResult.executionState = executionState;
            WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
            return Ok(apiResponse);
        }
        else
        {
            responseResult.entity = null;
            responseResult.message = message;
            responseResult.executionState = executionState;

            WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
            return Ok(apiResponse);
        }
    }


    #region Sending Email

    [HttpPost("send")]
    [AllowAnonymous]
    public async Task<IActionResult> SendEmail(MailRequest email)
    {
        if (ModelState.IsValid)
        {
            try
            {

                // Set the sender email
                await _messageService.SendEmailAsync(email);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        return BadRequest(ModelState);
    }
    #endregion



    #region OTP
    [HttpPost("VerifyOTP")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<LoginResultVM>>> VerifyOTP([FromBody] OtpverificationVM model)
    {
        try
        {
            (ExecutionState executionState, LoginResultVM entity, string message) responseResult;
            responseResult.entity = null;
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!string.IsNullOrEmpty(model.UserEmail))
            {

                (ExecutionState executionState, UserVM? entity, string? refreshToken, string message) userresult = await _userService.GetByEmail(model.UserEmail);
                if (userresult.executionState != ExecutionState.Failure)
                {
                    string? validOtp = await _userService.GetOtp(userresult.entity?.Id!);
                    if (!string.IsNullOrEmpty(model.OTP) && model.OTP == validOtp)
                    {
                        UserVM userVM = userresult.entity;
                        userVM.EmailConfirmed = !userresult.entity.EmailConfirmed ? true : userresult.entity.EmailConfirmed;
                        (ExecutionState executionState, UserVM? entity, string message) updateUser = await _userService.UpdateAsync(userVM);

                        if (updateUser.executionState == ExecutionState.Success)
                        {
                            LoginResultVM loginResult = new()
                            {
                                Id = updateUser.entity?.Id,
                                UserName = updateUser.entity?.UserName,
                                AccessToken = _generateJSONWebToken.GetToken(updateUser.entity, false),
                                RefreshToken = userresult.refreshToken,
                                FullName = updateUser.entity?.FullName,
                                Email = updateUser.entity?.Email,
                                IsOtpVerified = true

                            };
                            responseResult.entity = loginResult;
                        }
                        responseResult.message = updateUser.message;
                        responseResult.executionState = updateUser.executionState;
                        WebApiResponse<LoginResultVM> apiResponse = new WebApiResponse<LoginResultVM>(responseResult);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        responseResult.message = "OTP not valid";
                        responseResult.entity = null;
                        responseResult.executionState = ExecutionState.Failure;
                        WebApiResponse<LoginResultVM> apiResponse = new WebApiResponse<LoginResultVM>(responseResult);

                        return NotFound(apiResponse);
                    }
                }

            }

            responseResult.message = "Not Found";
            responseResult.executionState = ExecutionState.Failure;
            return NotFound(responseResult);

        }
        catch (Exception e)
        {

            throw;
        }
    }
    #endregion




    [HttpPost("Register")]
    [AllowAnonymous]
    [ModelStateValidate]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<LoginResultVM>>> Register([FromBody] UserRegisterModel model)
    {

        (ExecutionState executionState, UserVM? entity, string message) = await _userService.Register(model);
        (ExecutionState executionState, LoginResultVM entity, string message) responseResult;

        if (executionState == ExecutionState.Success && entity is not null)
        {
            var otp = await _userService.GetOtp(entity.Id!);

            var email = new MailRequest
            {
                ToEmail = entity.Email,
                OTP = otp
            };
            //await _messageService.SendEmailAsync(email);
            LoginResultVM loginResult = new()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                FullName = entity.FullName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber
            };

            responseResult.entity = loginResult;
            responseResult.message = message;
            responseResult.executionState = executionState;
            WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
            return Ok(apiResponse);
        }
        else
        {
            responseResult.entity = null;
            responseResult.message = message;
            responseResult.executionState = executionState;

            WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
            return Ok(apiResponse);
        }
    }

    [HttpPut]
    [AllowAnonymous]
    [ModelStateValidate]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<LoginResultVM>>> UpdateAsync([FromBody] UserUpdateVM userUpdateVM)
    {

        var model = _userService.CastToUserVM(userUpdateVM);
        (ExecutionState executionState, UserVM? entity, string message) = await _userService.UpdateAsync(model);
        (ExecutionState executionState, LoginResultVM entity, string message) responseResult;

        if (executionState == ExecutionState.Success && entity is not null)
        {
            LoginResultVM loginResult = new()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                FullName = entity.FullName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber
            };

            responseResult.entity = loginResult;
            responseResult.message = message;
            responseResult.executionState = executionState;
            WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
            return Ok(apiResponse);
        }
        else
        {
            responseResult.entity = null;
            responseResult.message = message;
            responseResult.executionState = executionState;

            WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
            return Ok(apiResponse);
        }
    }

    [HttpPost("{userId}/ProfilePicture")]
    [Authorize]
    [ModelStateValidate]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> SaveProfilePicture([FromRoute] string userId, IFormFile profilePicture)
    {
        if (HttpContext.User.FindFirst("UserId")?.Value != userId)
        {
            return Unauthorized();
        }

        WebApiResponse<UserProfilePictureUploadResponseVM> apiResponse;

        if (profilePicture.Length <= 0)
        {
            apiResponse = new WebApiResponse<UserProfilePictureUploadResponseVM>((ExecutionState.Success, null!, "Please select an image"));
            return Ok(apiResponse);
        }

        var fileHelper = new FileHelper(_webHostEnvironment);

        if (!fileHelper.IsValidImage(profilePicture))
        {
            apiResponse = new WebApiResponse<UserProfilePictureUploadResponseVM>((ExecutionState.Failure, null!, "Not a valid image"));
            return BadRequest(apiResponse);
        }

        using var memoryStream = new MemoryStream();
        await profilePicture.OpenReadStream().CopyToAsync(memoryStream);

        // Upload the file if less than 5 MB
        if (memoryStream.Length > 5_242_880)
        {
            apiResponse = new WebApiResponse<UserProfilePictureUploadResponseVM>((ExecutionState.Success, null!, "Image should be under 5 MB"));
            return Ok(apiResponse);
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            apiResponse = new WebApiResponse<UserProfilePictureUploadResponseVM>((ExecutionState.Success, null!, "Invalid User"));
            return Ok(apiResponse);
        }

        (bool success, string profilePictureUrl, string fileName, string message) fileUploadResult;

        if (user != null && !user.ProfilePictureUrl.IsNullOrEmpty())
        {
            // If profile picture exists then delete the previous one
            _ = fileHelper.DeleteFile(user.ProfilePictureUrl);
        }

        fileUploadResult = fileHelper.SaveFile(profilePicture, FileType.Image, DirectoryNames.USER_PROFILE_PICTURE_DIRECTORY_NAME, userId);

        if (!fileUploadResult.success)
        {
            apiResponse = new WebApiResponse<UserProfilePictureUploadResponseVM>((ExecutionState.Failure, null!, "Failed to save profile picture"));
            return Ok(apiResponse);
        }

        apiResponse = await UserProfilePictureUrlUpdate(fileUploadResult.profilePictureUrl, fileUploadResult.fileName, user);
        return Ok(apiResponse);
    }

    [HttpGet("{userId}/ProfilePicture")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetProfilePicture([FromRoute] string userId)
    {
        try
        {
            User? user = await _userManager.FindByIdAsync(userId);
            if (user != null && !user.ProfilePictureUrl.IsNullOrEmpty())
            {
                var fileHelper = new FileHelper(_webHostEnvironment);
                (byte[] bytes, string contentType, string fileName, string message) fileResult = fileHelper.GetFile(user.ProfilePictureUrl);
                return File(fileResult.bytes, fileResult.contentType, $"{fileResult.fileName}");
            }
            else
            {
                return NoContent();
            }
        }
        catch (Exception ex)
        {
            return NoContent();
        }
    }

    [HttpDelete("{userId}/ProfilePicture")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProfilePicture([FromRoute] string userId)
    {
        WebApiResponse<UserProfilePictureUploadResponseVM> apiResponse;
        try
        {
            if (HttpContext.User.FindFirst("UserId")?.Value != userId)
            {
                return Unauthorized();
            }

            User? user = await _userManager.FindByIdAsync(userId);
            if (user != null && !user.ProfilePictureUrl.IsNullOrEmpty())
            {
                var fileHelper = new FileHelper(_webHostEnvironment);
                var fileResult = fileHelper.DeleteFile(user.ProfilePictureUrl);

                apiResponse = await UserProfilePictureUrlUpdate(null, null, user);
                return Ok(apiResponse);
            }
            else
            {
                apiResponse = await UserProfilePictureUrlUpdate(null, null, user);
                return Ok(apiResponse);
            }
        }
        catch (Exception ex)
        {
            return NoContent();
        }
    }

    [HttpGet("{userId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetUserDetails([FromRoute] string userId)
    {
        WebApiResponse<UserVM> apiResponse;
        try
        {
            if (HttpContext.User.FindFirst("UserId")?.Value != userId)
            {
                return Unauthorized();
            }

            (ExecutionState executionState, UserVM? entity, string message) userResult = await _userService.GetById(userId);
            apiResponse = new WebApiResponse<UserVM>((userResult.executionState, userResult.entity!, userResult.message));

            if (userResult.executionState != ExecutionState.Failure)
            {
                return Ok(apiResponse);
            }
            else
            {
                apiResponse = new WebApiResponse<UserVM>((userResult.executionState, null!, "User not found"));
                return Ok(apiResponse);
            }
        }
        catch (Exception ex)
        {
            return NoContent();
        }
    }


    [NonAction]
    private async Task<WebApiResponse<UserProfilePictureUploadResponseVM>> UserProfilePictureUrlUpdate(string? profilePictureUrl, string? fileName, User user)
    {
        WebApiResponse<UserProfilePictureUploadResponseVM> apiResponse;

        var userProfilePictureUploadResponse = new UserProfilePictureUploadResponseVM()
        {
            ProfilePictureUrl = profilePictureUrl,
            FileName = fileName
        };

        user.ProfilePictureUrl = profilePictureUrl;
        var userUpdateResult = await _userManager.UpdateAsync(user);

        if (!userUpdateResult.Succeeded)
        {
            apiResponse = new WebApiResponse<UserProfilePictureUploadResponseVM>((ExecutionState.Failure, userProfilePictureUploadResponse, "Failed to update user"));
        }
        else
        {
            apiResponse = new WebApiResponse<UserProfilePictureUploadResponseVM>((ExecutionState.Success, userProfilePictureUploadResponse, "Successfully saved user"));
        }

        return apiResponse;
    }

    [HttpPost("AccessToken")]
    [AllowAnonymous]
    [ModelStateValidate]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<LoginResultVM>>> AccessToken([FromQuery] LoginVM model)
    {

        (ExecutionState executionState, UserVM? entity, string? refreshToken, string message) = await _userService.Login(model);
        (ExecutionState executionState, LoginResultVM entity, string message) responseResult;

        if (executionState == ExecutionState.Success && entity is not null)
        {
            LoginResultVM loginResult = new()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                AccessToken = _generateJSONWebToken.GetToken(entity, model.RememberMe),
                RefreshToken = refreshToken,
                FullName = entity.FullName
            };

            responseResult.entity = loginResult;
            responseResult.message = message;
            responseResult.executionState = executionState;
            WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
            return Ok(apiResponse.Data.AccessToken);
        }
        else
        {
            responseResult.entity = null;
            responseResult.message = message;
            responseResult.executionState = executionState;

            WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
            return Ok("");
        }
    }


    [HttpPut("ChangePassword")]
    [AllowAnonymous]
    [ModelStateValidate]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<LoginVM>>> ChangePassword([FromBody] UserChangePasswordVM model)
    {
        try
        {

            (ExecutionState executionState, LoginResultVM entity, string message) responseResult;

            if (!model.NewPassword.Equals(model.ConfirmPassword))
            {
                responseResult.entity = null;
                responseResult.message = "New password and confirm password didn't match";
                responseResult.executionState = ExecutionState.Failure;

                WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
                return Ok(apiResponse);
            }
            (ExecutionState executionState, LoginResultVM? entity, string message) = await _userService.ChangePasswordAsync(model);

            if (executionState == ExecutionState.Success && entity is not null)
            {
                LoginResultVM loginResult = new()
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    FullName = entity.FullName,
                    Email = entity.Email,
                };

                responseResult.entity = loginResult;
                responseResult.message = message;
                responseResult.executionState = executionState;
                WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
                return Ok(apiResponse);
            }
            else
            {
                responseResult.entity = null;
                responseResult.message = message;
                responseResult.executionState = ExecutionState.Failure;

                WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
                return Ok(apiResponse);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpPost("ValidateCredential")]
    [AllowAnonymous]
    [ModelStateValidate]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<UserCredentialValidationVM>>> ValidateCredential([FromBody] UserCredentialValidationVM model)
    {

        (ExecutionState executionState, UserCredentialValidationVM? entity, string message) = await _userService.ValidateCredential(model);
        (ExecutionState executionState, UserCredentialValidationVM entity, string message) responseResult;

        if (executionState == ExecutionState.Success && entity is not null)
        {
            UserCredentialValidationVM userCredentialValidationVM = new()
            {
                UserName = entity.UserName,
                Email = entity.Email
            };

            responseResult.entity = userCredentialValidationVM;
            responseResult.message = message;
            responseResult.executionState = executionState;
            WebApiResponse<UserCredentialValidationVM> apiResponse = new(responseResult);
            return Ok(apiResponse);
        }
        else
        {
            responseResult.entity = null;
            responseResult.message = message;
            responseResult.executionState = executionState;

            WebApiResponse<UserCredentialValidationVM> apiResponse = new(responseResult);
            return Ok("");
        }
    }

    [HttpGet("GetOtp")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<LoginResultVM>>> GetOTP(string userId)
    {


        (ExecutionState executionState, UserVM? entity, string message) = await _userService.GetById(userId);
        (ExecutionState executionState, LoginResultVM entity, string message) responseResult;

        if (executionState == ExecutionState.Success && entity is not null)
        {
            LoginResultVM loginResult = new()
            {
                Email = entity.Email,
                OTP = await _userService.GetOtp(entity.Id!)
            };

            responseResult.entity = loginResult;
            responseResult.message = message;
            responseResult.executionState = executionState;
            WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
            return Ok(apiResponse);
        }
        else
        {
            responseResult.entity = null;
            responseResult.message = message;
            responseResult.executionState = executionState;

            WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
            return Ok(apiResponse);
        }

    }
    #region OTP
    [HttpPost("ResendOTP")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<LoginResultVM>>> ResendOTP([FromBody] ResendOtpVM model)
    {
        try
        {
            (ExecutionState executionState, LoginResultVM entity, string message) responseResult;
            responseResult.entity = null;
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!string.IsNullOrEmpty(model.UserEmail))
            {

                (ExecutionState executionState, UserVM? entity, string? refreshToken, string message) userresult = await _userService.GetByEmail(model.UserEmail);
                if (userresult.executionState != ExecutionState.Failure)
                {

                    await _cache.SetAsync(userresult.entity.Id!, _userService.GenerateOTP());
                    var otp = await _userService.GetOtp(userresult.entity.Id!);

                    var email = new MailRequest
                    {
                        ToEmail = userresult.entity.Email,
                        OTP = otp
                    };

                    if (!string.IsNullOrWhiteSpace(otp))
                    {
                        await _messageService.SendEmailAsync(email);

                        LoginResultVM loginResult = new()
                        {
                            Id = userresult.entity.Id,
                            UserName = userresult.entity.UserName,
                            FullName = userresult.entity.FullName,
                            Email = userresult.entity.Email
                        };

                        responseResult.entity = loginResult;
                        responseResult.message = userresult.message;
                        responseResult.executionState = userresult.executionState;
                        WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        responseResult.message = "Failed to create new OTP";
                        responseResult.entity = null;
                        responseResult.executionState = ExecutionState.Failure;
                        WebApiResponse<LoginResultVM> apiResponse = new WebApiResponse<LoginResultVM>(responseResult);

                        return NotFound(apiResponse);
                    }
                }
                else
                {
                    responseResult.message = "User not found with this email";
                    responseResult.entity = null;
                    responseResult.executionState = ExecutionState.Failure;
                    WebApiResponse<LoginResultVM> apiResponse = new WebApiResponse<LoginResultVM>(responseResult);

                    return NotFound(apiResponse);
                }
            }

            else if (!string.IsNullOrEmpty(model.MobileNumber))
            {

                (ExecutionState executionState, UserVM? entity, string? refreshToken, string message) userresult = await _userService.GetByMobile(model.MobileNumber);
                if (userresult.executionState != ExecutionState.Failure)
                {
                    await _cache.SetAsync(userresult.entity.Id!, _userService.GenerateOTP());
                    string otp = await _userService.GetOtp(userresult.entity.Id!);
                    var email = new MailRequest
                    {
                        ToEmail = userresult.entity.Email,
                        OTP = otp
                    };

                    if (!string.IsNullOrWhiteSpace(otp))
                    {
                        //await _messageService.SendEmailAsync(email);
                        LoginResultVM loginResult = new()
                        {
                            Id = userresult.entity.Id,
                            UserName = userresult.entity.UserName,
                            FullName = userresult.entity.FullName
                        };

                        responseResult.entity = loginResult;
                        responseResult.message = userresult.message;
                        responseResult.executionState = userresult.executionState;
                        WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        responseResult.message = "Failed to create new OTP";
                        responseResult.entity = null;
                        responseResult.executionState = ExecutionState.Failure;
                        WebApiResponse<LoginResultVM> apiResponse = new WebApiResponse<LoginResultVM>(responseResult);

                        return NotFound(apiResponse);
                    }
                }
                else
                {
                    responseResult.message = "User not found with this mobile number";
                    responseResult.entity = null;
                    responseResult.executionState = ExecutionState.Failure;
                    WebApiResponse<LoginResultVM> apiResponse = new WebApiResponse<LoginResultVM>(responseResult);

                    return NotFound(apiResponse);
                }
            }

            responseResult.message = "User not Found";
            responseResult.executionState = ExecutionState.Failure;
            return NotFound(responseResult);

        }
        catch (Exception e)
        {

            throw;
        }
    }
    #endregion

    [HttpPost("ForgotPassword")]
    [AllowAnonymous]
    [ModelStateValidate]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<LoginResultVM>>> ForgotPassword([FromBody] ForgotPasswordVM model)
    {
        try
        {
            (ExecutionState executionState, LoginResultVM entity, string message) responseResult;
            responseResult.entity = null;

            if (!model.NewPassword.Equals(model.ConfirmPassword))
            {
                responseResult.entity = null;
                responseResult.message = "New password and confirm password didn't match";
                responseResult.executionState = ExecutionState.Failure;

                WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
                return Ok(apiResponse);
            }

            (ExecutionState executionState, UserVM? entity, string? refreshToken, string message) userresult = await _userService.GetByEmail(model.Email);
            if (userresult.executionState != ExecutionState.Failure && userresult.entity is not null)
            {
                string otp = await _userService.GetOtp(userresult.entity.Id!);

                var email = new MailRequest
                {
                    ToEmail = userresult.entity.Email,
                    OTP = otp
                };

                if (!model.Otp.Equals(otp) || string.IsNullOrWhiteSpace(otp))
                {
                    responseResult.message = "OTP is not valid";
                    responseResult.entity = null;
                    responseResult.executionState = ExecutionState.Failure;
                    WebApiResponse<LoginResultVM> apiResponse = new WebApiResponse<LoginResultVM>(responseResult);

                    return NotFound(apiResponse);
                }
                (ExecutionState executionState, UserVM? entity, string message) = await _userService.ResetPasswordAsync(userresult.entity, model.NewPassword);

                if (executionState == ExecutionState.Success && entity is not null)
                {
                    LoginResultVM loginResult = new()
                    {
                        Id = entity.Id,
                        UserName = entity.UserName,
                        FullName = entity.FullName,
                        Email = entity.Email
                    };

                    responseResult.entity = loginResult;
                    responseResult.message = message;
                    responseResult.executionState = executionState;
                    WebApiResponse<LoginResultVM> apiResponse = new(responseResult);
                    return Ok(apiResponse);

                }
                else
                {
                    responseResult.message = "Failed to update password";
                    responseResult.entity = null;
                    responseResult.executionState = ExecutionState.Failure;
                    WebApiResponse<LoginResultVM> apiResponse = new WebApiResponse<LoginResultVM>(responseResult);

                    return NotFound(apiResponse);
                }
            }

            responseResult.message = "User not Found";
            responseResult.executionState = ExecutionState.Failure;
            return NotFound(responseResult);
        }

        catch (Exception e)
        {

            throw;
        }
    }

}

