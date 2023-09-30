using System;
using System.Threading.Tasks;

using PTSL.Ovidhan.Common.Entity.UserEntitys;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model;
using PTSL.Ovidhan.Common.Model.EntityViewModels.SystemUser;

namespace PTSL.Ovidhan.Service.Services.UserEntitys;

public interface IRefreshTokenService
{
    Task<(ExecutionState executionState, bool entity, string message)> RevokeAsync(string token, string userId);
    Task<(ExecutionState executionState, bool entity, string message)> RemoveOutdatedAsync();
    Task<(ExecutionState executionState, RefreshToken entity, string message)> CreateAsync(string userId, DateTime expiresAt, string token);
    Task<(ExecutionState executionState, LoginResultVM? entity, string message)> GetNewTokenAsync(UserTokenRequestResponseVM model);
}