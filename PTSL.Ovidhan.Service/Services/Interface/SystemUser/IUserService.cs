using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model;
using PTSL.Ovidhan.Common.Model.EntityViewModels.SystemUser;

namespace PTSL.Ovidhan.Service.Services.Interface;

public interface IUserService
{
    Task<(ExecutionState executionState, UserVM? entity, string? refreshToken, string message)> Login(LoginVM model);
    Task<(ExecutionState executionState, UserVM? entity, string message)> Register(UserRegisterModel model);
    Task<(ExecutionState executionState, List<UserDropdownVM> entity, string message)> UserLists();
    Task<(ExecutionState executionState, long TodaysUserListsCount, string message)> TodaysUserListsCount();
    Task<(ExecutionState executionState, long TotalUserCount, string message)> TotalUserCount();
    Task<(ExecutionState executionState, UserVM? entity, string message)> GetById(string userId);
    Task<(ExecutionState executionState, UserVM? entity, string? refreshToken, string message)> GetByEmail(string email);
    Task<(ExecutionState executionState, UserVM? entity, string? refreshToken, string message)> GetByMobile(string mobileNo);
    Task<(ExecutionState executionState, UserVM? entity, string message)> UpdateAsync(UserVM user);
    Task<(ExecutionState executionState, UserVM? entity, string message)> DeleteAsync(UserVM user);
    Task<(ExecutionState executionState, LoginResultVM? entity, string message)> ChangePasswordAsync(UserChangePasswordVM user);
    Task<(ExecutionState executionState, UserVM? entity, string message)> ResetPasswordAsync(UserVM user,string password);
    Task<(ExecutionState executionState, UserCredentialValidationVM? entity, string message)> ValidateCredential(UserCredentialValidationVM model);
    UserVM CastToUserVM(UserUpdateVM user);
    Task<string> GetOtp(string id);
    string GenerateOTP();


}
