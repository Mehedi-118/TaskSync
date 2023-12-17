using System;
using System.Threading.Tasks;

using AutoMapper;

using PTSL.Ovidhan.Business.Businesses.Interface;
using PTSL.Ovidhan.Business.Businesses.Interface.UserEntitys;
using PTSL.Ovidhan.Business.TokenHelper;
using PTSL.Ovidhan.Common.Entity.UserEntitys;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model;
using PTSL.Ovidhan.Common.Model.EntityViewModels.SystemUser;

namespace PTSL.Ovidhan.Service.Services.UserEntitys
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenBusiness _business;
        public IMapper _mapper;
        private readonly IUserBusiness _userBusiness;
        private readonly IGenerateJSONWebToken _generateJSONWebToken;
        private readonly IRefreshTokenBusiness _refreshTokenBusiness;

        public RefreshTokenService(IRefreshTokenBusiness business, IMapper mapper, IUserBusiness userBusiness, IGenerateJSONWebToken generateJSONWebToken, IRefreshTokenBusiness refreshTokenBusiness)
        {
            _business = business;
            _mapper = mapper;
            _userBusiness = userBusiness;
            _generateJSONWebToken = generateJSONWebToken;
            _refreshTokenBusiness = refreshTokenBusiness;
        }

        public Task<(ExecutionState executionState, RefreshToken entity, string message)> CreateAsync(string userId, DateTime expiresAt, string token)
        {
            return _business.CreateAsync(userId, expiresAt, token);
        }

        public async Task<(ExecutionState executionState, LoginResultVM? entity, string message)> GetNewTokenAsync(UserTokenRequestResponseVM model)
        {
            var userIdClaim = TokenDecoder.GetClaim(model.AccessToken, CustomClaimTypes.UserId);
            if (userIdClaim is null)
            {
                return (ExecutionState.Failure, null, "Invalid access token");
            }

            var isRememberMeClaim = TokenDecoder.GetClaim(model.AccessToken, CustomClaimTypes.RememberMe);
            if (isRememberMeClaim is null)
            {
                return (ExecutionState.Failure, null, "Invalid access token");
            }

            var userId = userIdClaim.Value;
            _ = bool.TryParse(isRememberMeClaim.Value, out var rememberMe);

            var user = await _userBusiness.GetById(userId);
            if (user.entity is null)
            {
                return (ExecutionState.Failure, null, "User not found");
            }

            var refreshToken = await _refreshTokenBusiness.GetAsync(userId, model.RefreshToken);
            if (refreshToken.entity is null)
            {
                return (ExecutionState.Failure, null, "Token not found");
            }

            if (rememberMe)
            {
                refreshToken.entity.ExpiresAt = refreshToken.entity.ExpiresAt.AddDays(1);
                await _refreshTokenBusiness.UpdateAsync(refreshToken.entity);
            }

            //var result = new UserTokenRequestResponseVM(_generateJSONWebToken.GetToken(_mapper.Map<UserVM>(user.entity), rememberMe), null!);

            var tknR = _generateJSONWebToken.GetToken(_mapper.Map<UserVM>(user.entity), rememberMe);
            var result = new LoginResultVM
            {
                Id = user.entity.Id,
                FullName = user.entity.FullName,
                UserName = user.entity.UserName,
                AccessToken = tknR,
                RefreshToken = refreshToken.entity.Token,
            };
            return (ExecutionState.Success, result, "Done");
        }

        public Task<(ExecutionState executionState, bool entity, string message)> RemoveOutdatedAsync()
        {
            return _business.RemoveOutdatedAsync();
        }

        public Task<(ExecutionState executionState, bool entity, string message)> RevokeAsync(string token, string userId)
        {
            return _business.RevokeAsync(token, userId);
        }
    }
}