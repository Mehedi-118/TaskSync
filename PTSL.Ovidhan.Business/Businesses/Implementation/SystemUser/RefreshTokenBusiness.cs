using System;
using System.Threading.Tasks;

using PTSL.Ovidhan.Business.Businesses.Interface.UserEntitys;
using PTSL.Ovidhan.Common.Entity.UserEntitys;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.DAL.Repositories.Interface.UserEntitys;

namespace PTSL.Ovidhan.Business.Businesses.Implementation.UserEntitys;

public class RefreshTokenBusiness : IRefreshTokenBusiness
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokenBusiness(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public Task<(ExecutionState executionState, RefreshToken entity, string message)> CreateAsync(string userId, DateTime expiresAt, string token)
    {
        return _refreshTokenRepository.CreateAsync(userId, expiresAt, token);
    }

    public Task<(ExecutionState executionState, RefreshToken entity, string message)> GetAsync(string userId, string token)
    {
        return _refreshTokenRepository.GetAsync(userId, token);
    }

    public Task<(ExecutionState executionState, bool entity, string message)> RemoveOutdatedAsync()
    {
        return _refreshTokenRepository.RemoveOutdatedAsync();
    }

    public Task<(ExecutionState executionState, bool entity, string message)> RevokeAsync(string token, string userId)
    {
        return _refreshTokenRepository.RevokeAsync(token, userId);
    }

    public Task<(ExecutionState executionState, RefreshToken entity, string message)> UpdateAsync(RefreshToken token)
    {
        return _refreshTokenRepository.UpdateAsync(token);
    }
}