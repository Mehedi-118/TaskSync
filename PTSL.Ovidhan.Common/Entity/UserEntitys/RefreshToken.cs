using System;

namespace PTSL.Ovidhan.Common.Entity.UserEntitys;

public class RefreshToken
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public string? DeviceInfo { get; set; }

    public RefreshToken()
    {
        Id = Guid.NewGuid().ToString();
    }
}

