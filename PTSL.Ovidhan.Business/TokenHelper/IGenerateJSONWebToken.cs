using Microsoft.Extensions.Configuration;

using PTSL.Ovidhan.Common.Model;

namespace PTSL.Ovidhan.Business.TokenHelper
{
    public interface IGenerateJSONWebToken
    {
        string GenerateRefreshToken();
        string GetToken(UserVM? userInfo, bool rememberMe);
    }
}