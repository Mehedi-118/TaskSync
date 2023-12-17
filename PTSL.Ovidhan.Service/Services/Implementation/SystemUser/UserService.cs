using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.Extensions.Caching.Distributed;

using PTSL.Ovidhan.Business.Businesses.Interface;
using PTSL.Ovidhan.Business.TokenHelper;
using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Entity.UserEntitys;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Helper;
using PTSL.Ovidhan.Common.Model;
using PTSL.Ovidhan.Common.Model.EntityViewModels.SystemUser;
using PTSL.Ovidhan.Service.Services.Interface;
using PTSL.Ovidhan.Service.Services.UserEntitys;

namespace PTSL.Ovidhan.Service.Services.Implementation;

public class UserService : IUserService
{
    public readonly IUserBusiness _userBusiness;

    public IMapper _mapper;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IGenerateJSONWebToken _generateJSONWebToken;
    private readonly ICacheService _cache;
    private readonly string cacheKey = "OTP_";


    public UserService(IUserBusiness userBusiness, IMapper mapper, IRefreshTokenService refreshTokenService, IGenerateJSONWebToken generateJSONWebToken, ICacheService cache)
    {
        _userBusiness = userBusiness;
        _mapper = mapper;
        _refreshTokenService = refreshTokenService;
        _generateJSONWebToken = generateJSONWebToken;
        _cache = cache;
    }

    public UserVM CastToUserVM(UserUpdateVM user)
    {
        try
        {
            return _mapper.Map<UserVM>(user);

        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public async Task<(ExecutionState executionState, UserVM? entity, string? refreshToken, string message)> Login(LoginVM model)
    {
        (ExecutionState executionState, User? entity, string message) loginResult = await _userBusiness.Login(model);
        if (loginResult.executionState == ExecutionState.Success)
        {
            UserVM? userVM = _mapper.Map<UserVM>(loginResult.entity);
            (ExecutionState executionState, RefreshToken entity, string message) tokenResult;
            if (loginResult.executionState !=ExecutionState.Failure)
            {
                tokenResult = await _refreshTokenService.CreateAsync(loginResult.entity!.Id, DateTime.Now.AddDays(1), _generateJSONWebToken.GenerateRefreshToken());

                return (loginResult.executionState, userVM, tokenResult.entity.Token, loginResult.message);
            }
           

            return (loginResult.executionState, userVM, null, loginResult.message);

        }
        else if (loginResult.executionState == ExecutionState.Failure)
        {
            return (loginResult.executionState, _mapper.Map<UserVM?>(loginResult.entity), null, loginResult.message);
        }
        return (loginResult.executionState, null, null, loginResult.message);
    }

    public async Task<(ExecutionState executionState, List<UserDropdownVM> entity, string message)> UserLists()
    {
        var result = await _userBusiness.UserLists();
        return (result.executionState, _mapper.Map<List<UserDropdownVM>>(result.entity), result.message);
    }

    public async Task<(ExecutionState executionState, UserVM? entity, string message)> Register(UserRegisterModel model)
    {
        (ExecutionState executionState, User? entity, string message) result = await _userBusiness.Register(model);
        if (result.executionState == ExecutionState.Success)
        {
            await _cache.SetAsync(result.entity!.Id, GenerateOTP());
            return (result.executionState, _mapper.Map<UserVM>(result.entity), result.message);
        }

        return (result.executionState, null, result.message);
    }

    public async Task<(ExecutionState executionState, UserVM? entity, string message)> GetById(string userId)
    {
        var result = await _userBusiness.GetById(userId);
        return (result.executionState, _mapper.Map<UserVM>(result.entity), result.message);
    }

    public async Task<(ExecutionState executionState, UserVM? entity, string message)> UpdateAsync(UserVM user)
    {
        (ExecutionState executionState, User? entity, string message) result = await _userBusiness.UpdateAsync(_mapper.Map<User>(user));
        return (result.executionState, _mapper.Map<UserVM>(result.entity), result.message);
    }
    public async Task<(ExecutionState executionState, UserVM? entity, string message)> DeleteAsync(UserVM user)
    {
        (ExecutionState executionState, User? entity, string message) result = await _userBusiness.DeleteAsync(_mapper.Map<User>(user));
        return (result.executionState, _mapper.Map<UserVM>(result.entity), result.message);
    }

    public async Task<(ExecutionState executionState, LoginResultVM? entity, string message)> ChangePasswordAsync(UserChangePasswordVM user)
    {

        var result = await _userBusiness.ChangePasswordAsync(user);
        return (result.executionState, _mapper.Map<LoginResultVM>(result.entity), result.message);
    }

    public async Task<(ExecutionState executionState, UserCredentialValidationVM? entity, string message)> ValidateCredential(UserCredentialValidationVM model)
    {
        var result = await _userBusiness.ValidateCredential(model);
        return (result.executionState, result.entity, result.message);
    }

    public async Task<(ExecutionState executionState, UserVM? entity, string? refreshToken, string message)> GetByEmail(string email)
    {
        (ExecutionState executionState, User? entity, string message) result = await _userBusiness.GetByEmail(email);

        if (result.executionState == ExecutionState.Success)
        {
            (ExecutionState executionState, RefreshToken entity, string message) tokenResult = await _refreshTokenService.CreateAsync(result.entity!.Id, DateTime.Now.AddDays(1), _generateJSONWebToken.GenerateRefreshToken());
            return (result.executionState, _mapper.Map<UserVM?>(result.entity), tokenResult.entity.Token, result.message);
        }
        return (result.executionState, null, null, result.message);
    }
    public async Task<(ExecutionState executionState, UserVM? entity, string? refreshToken, string message)> GetByMobile(string mobileNo)
    {
        (ExecutionState executionState, User? entity, string message) result = _userBusiness.GetByMobile(mobileNo);

        if (result.executionState == ExecutionState.Success)
        {
            (ExecutionState executionState, RefreshToken entity, string message) tokenResult = await _refreshTokenService.CreateAsync(result.entity!.Id, DateTime.Now.AddDays(1), _generateJSONWebToken.GenerateRefreshToken());
            return (result.executionState, _mapper.Map<UserVM?>(result.entity), tokenResult.entity.Token, result.message);
        }
        return (result.executionState, null, null, result.message);
    }

    public async Task<string> GetOtp(string userId)
    {
        byte[] retrievedOtp = await _cache.GetAsync(userId);
        return ConverterService.ByteArrayToObject<string>(retrievedOtp);
    }

    public string GenerateOTP()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        var otp = _rdm.Next(_min, _max).ToString();
        return otp;

    }

    public async Task<(ExecutionState executionState, UserVM? entity, string message)> ResetPasswordAsync(UserVM user, string password)
    {
        (ExecutionState executionState, User? entity, string message) result = await _userBusiness.ResetPasswordAsync(_mapper.Map<User>(user), password);
        return (result.executionState, _mapper.Map<UserVM>(result.entity), result.message);
    }

    public async Task<(ExecutionState executionState, long TodaysUserListsCount, string message)> TodaysUserListsCount()
    {
        (ExecutionState executionState, long TodaysUserListsCount, string message) result = await _userBusiness.TodaysUserListsCount();
        return (result.executionState, result.TodaysUserListsCount, result.message);
    }

    public async Task<(ExecutionState executionState, long TotalUserCount, string message)> TotalUserCount()
    {
        (ExecutionState executionState, long TotalUserCount, string message) result = await _userBusiness.TotalUserCount();
        return (result.executionState, result.TotalUserCount, result.message);
    }
}
