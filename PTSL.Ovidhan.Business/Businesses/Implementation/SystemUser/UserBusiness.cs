using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using PTSL.Ovidhan.Business.Businesses.Interface;
using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model;
using PTSL.Ovidhan.Common.Model.EntityViewModels.SystemUser;

namespace PTSL.Ovidhan.Business.Businesses.Implementation;

public class UserBusiness : IUserBusiness
{
    private readonly UserManager<User> _userManager;

    public UserBusiness(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<(ExecutionState executionState, User? entity, string message)> Login(LoginVM model)
    {
        bool isEmail = false;
        if (model.UserName.Contains("@")) isEmail = true;

        User? user;

        if (isEmail)
        {
            user = await _userManager.FindByEmailAsync(model.UserName);
        }
        else
        {
            user = await _userManager.FindByNameAsync(model.UserName);
        }

        if (user is null)
        {
            return (ExecutionState.Failure, null, "Username or password is invalid");
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(user, model.UserPassword);
        if (isValidPassword == false)
        {
            return (ExecutionState.Failure, null, "Username or password is invalid");
        }

        return (ExecutionState.Success, user, "Logged in successfully");
    }

    public async Task<(ExecutionState executionState, List<User> entity, string message)> UserLists()
    {
        var users = await _userManager.Users.ToListAsync();

        return (ExecutionState.Success, users, "Data retrieved successfully");
    }

    public async Task<(ExecutionState executionState, User? entity, string message)> GetById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return (ExecutionState.Failure, null, "Not found");
        }

        return (ExecutionState.Success, user, "Data retrieved successfully");
    }
    public async Task<(ExecutionState executionState, User? entity, string message)> GetByEmail(string email)
    {
        User? user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return (ExecutionState.Failure, null, "Not found");
        }

        return (ExecutionState.Success, user, "Data retrieved successfully");
    }
    public (ExecutionState executionState, User? entity, string message) GetByMobile(string mobileNo)
    {
        User? user = _userManager.Users.FirstOrDefault(e => e.PhoneNumber == mobileNo);
        if (user is null)
        {
            return (ExecutionState.Failure, null, "Not found");
        }

        return (ExecutionState.Success, user, "Data retrieved successfully");
    }

    public async Task<(ExecutionState executionState, User? entity, string message)> Register(UserRegisterModel model)
    {
        //validation
        var errorMsg = await GetDuplicateUserDataCount(model.UserName, model.Email); // get duplicate user email, username or nid count if any
        if (!errorMsg.IsNullOrEmpty())
        {
            return (ExecutionState.Failure, null, "Invalid " + errorMsg);

        }
        //
        var user = new User { UserName = model.UserName, Email = model.Email, FullName = model.FullName,Address = model.Address };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return (ExecutionState.Success, user, "Done");
        }

        return (ExecutionState.Failure, null, result.Errors.FirstOrDefault()?.Description ?? string.Empty);
    }

    private async Task<string> GetDuplicateUserDataCount(string userName, string email)
    {
        string msg = String.Empty;
        var duplicateUsernameCount = await _userManager.Users.CountAsync(u => u.UserName == userName);
        if (duplicateUsernameCount > 0) { msg += "Username "; }

        var duplicateEmailCount = await _userManager.Users.CountAsync(u => u.Email == email);
        if (duplicateEmailCount > 0) { msg += ((msg.IsNullOrEmpty() ? "" : "and ") + "Email "); }

        

        return msg;
    }

    public async Task<(ExecutionState executionState, User? entity, string message)> UpdateAsync(User model)
    {
        try
        {
            var user = await _userManager.Users.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
            if (user is not null)
            {
                (string userName, string nid, string email) tempData;

                tempData.userName = (user.UserName == model.UserName) ? null : model.UserName;
                tempData.email = (user.Email == model.Email) ? null : model.Email;



                //validation

                var errorMsg = await GetDuplicateUserDataCount(tempData.userName, tempData.email); // get duplicate user email, username or nid count if any
                if (!errorMsg.IsNullOrEmpty())
                {
                    return (ExecutionState.Failure, null, "Invalid " + errorMsg);

                }

            }
            if (user is null)
            {
                return (ExecutionState.Failure, null, "User not found");
            }
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FullName = model.FullName;
            user.ProfilePictureUrl = model.ProfilePictureUrl;
            user.DateOfBirth = model.DateOfBirth;
            user.Occupation = model.Occupation;
           
            user.EmailConfirmed = model.EmailConfirmed;
            user.PhoneNumberConfirmed = model.PhoneNumberConfirmed;
            user.Address = model.Address;


            // Update the user entity
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return (ExecutionState.Success, user, "Done");
            }
            return (ExecutionState.Failure, null, result.Errors.FirstOrDefault()?.Description ?? string.Empty);
        }
        catch (Exception ex)
        {

            return (ExecutionState.Failure, null, "Unable to update user");
        }
    }
    public async Task<(ExecutionState executionState, User? entity, string message)> DeleteAsync(User model)
    {
        try
        {
            var user = await _userManager.Users.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
           
            if (user is null)
            {
                return (ExecutionState.Failure, null, "User not found");
            }
           

            // Update the user entity
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return (ExecutionState.Success, user, "Done");
            }
            return (ExecutionState.Failure, null, result.Errors.FirstOrDefault()?.Description ?? string.Empty);
        }
        catch (Exception ex)
        {

            return (ExecutionState.Failure, null, "Unable to update user");
        }
    }

    public async Task<(ExecutionState executionState, User? entity, string message)> ChangePasswordAsync(UserChangePasswordVM model)
    {
        try
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                return (ExecutionState.Failure, null, "Username or password is invalid");
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, model.OldPassword);
            if (isValidPassword == false)
            {
                return (ExecutionState.Failure, null, "Username or password is invalid");
            }
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return (ExecutionState.Success, user, "Done");
            }
            return (ExecutionState.Failure, null, result.Errors.FirstOrDefault()?.Description ?? string.Empty);
        }
        catch (Exception ex) { return (ExecutionState.Failure, null, "Unable to change password"); }
    }

    public async Task<(ExecutionState executionState, UserCredentialValidationVM? entity, string message)> ValidateCredential(UserCredentialValidationVM model)
    {
        User? userName = await _userManager.FindByNameAsync(model.UserName);
        UserCredentialValidationVM userCredentialValidation = new();
        if (userName is not null)
        {
            userCredentialValidation.UserName = "User name already exists";

        }
        User? userEmail = await _userManager.FindByEmailAsync(model.Email);
        if (userEmail is not null)
        {
            userCredentialValidation.Email = "User Email already exists";

        }
        

        return (ExecutionState.Success, userCredentialValidation, string.Empty);
    }

    public async Task<(ExecutionState executionState, User? entity, string message)> ResetPasswordAsync(User model, string password)
    {
        try
        {


            var user = await _userManager.Users.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

            if (user is null)
            {
                return (ExecutionState.Failure, null, "User not found");
            }
            var updatePassword = _userManager.PasswordHasher.HashPassword(user, password);
            user.PasswordHash = updatePassword;
            await _userManager.UpdateSecurityStampAsync(user);

            // Update the user entity
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return (ExecutionState.Success, user, "Done");
            }
            return (ExecutionState.Failure, null, result.Errors.FirstOrDefault()?.Description ?? string.Empty);
        }
        catch (Exception ex)
        {
            return (ExecutionState.Failure, null, "Failed to change password");

        }

    }

    public async Task<(ExecutionState executionState, long TodaysUserListsCount, string message)> TodaysUserListsCount()
    {
        var users = await _userManager.Users.Where(e => e.CreatedAt!.Value.Date == DateTime.Now.Date).ToListAsync();


        return (ExecutionState.Success, users.Count, "Data retrieved successfully");
    }

    public async  Task<(ExecutionState executionState, long TotalUserCount, string message)> TotalUserCount()
    {
        List<User> totalUsers = await _userManager.Users.ToListAsync();


        return (ExecutionState.Success, totalUsers.Count, "Data retrieved successfully");
    }
}
