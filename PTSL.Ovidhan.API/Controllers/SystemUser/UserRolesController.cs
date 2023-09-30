using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PTSL.Ovidhan.Api.Controllers.SystemUser;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserRolesController
{
    public UserRolesController()
    {
    }
}
