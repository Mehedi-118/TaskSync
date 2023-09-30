using System.Collections.Generic;
using System.Threading.Tasks;

using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model;
using PTSL.Ovidhan.Common.Model.EntityViewModels.SystemUser;

namespace PTSL.Ovidhan.Business.Businesses.Interface;

public interface IUserBusiness
{
    Task<(ExecutionState executionState, User? entity, string message)> GetById(string userId);
    Task<(ExecutionState executionState, User? entity, string message)> GetByEmail(string email);
    (ExecutionState executionState, User? entity, string message) GetByMobile(string mobileNo);
    Task<(ExecutionState executionState, User? entity, string message)> Login(LoginVM model);
    Task<(ExecutionState executionState, User? entity, string message)> Register(UserRegisterModel model);
    Task<(ExecutionState executionState, List<User> entity, string message)> UserLists();
    Task<(ExecutionState executionState, long TodaysUserListsCount, string message)> TodaysUserListsCount();
    Task<(ExecutionState executionState, long TotalUserCount, string message)> TotalUserCount();
    Task<(ExecutionState executionState, User? entity, string message)> UpdateAsync(User model);
    Task<(ExecutionState executionState, User? entity, string message)> DeleteAsync(User user);

    Task<(ExecutionState executionState, User? entity, string message)> ChangePasswordAsync(UserChangePasswordVM user);
    Task<(ExecutionState executionState, User? entity, string message)> ResetPasswordAsync(User user, string password);
    Task<(ExecutionState executionState, UserCredentialValidationVM? entity, string message)> ValidateCredential(UserCredentialValidationVM model);
}

