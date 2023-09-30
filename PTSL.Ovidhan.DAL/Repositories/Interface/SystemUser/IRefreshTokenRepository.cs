using System;
using System.Threading.Tasks;

using PTSL.Ovidhan.Common.Entity.UserEntitys;
using PTSL.Ovidhan.Common.Enum;

namespace PTSL.Ovidhan.DAL.Repositories.Interface.UserEntitys
{
    public interface IRefreshTokenRepository
    {
        Task<(ExecutionState executionState, bool entity, string message)> RevokeAsync(string token, string userId);
        Task<(ExecutionState executionState, bool entity, string message)> RemoveOutdatedAsync();
        Task<(ExecutionState executionState, RefreshToken entity, string message)> CreateAsync(string userId, DateTime expiresAt, string token);
        Task<(ExecutionState executionState, RefreshToken entity, string message)> UpdateAsync(RefreshToken toke);
        Task<(ExecutionState executionState, RefreshToken entity, string message)> GetAsync(string userId, string token);
    }
}