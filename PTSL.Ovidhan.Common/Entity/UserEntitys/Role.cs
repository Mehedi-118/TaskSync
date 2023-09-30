using Microsoft.AspNetCore.Identity;

namespace PTSL.Ovidhan.Common.Entity;

public class Role : IdentityRole
{
    public string? RoleDesciption { get; set; }
}
