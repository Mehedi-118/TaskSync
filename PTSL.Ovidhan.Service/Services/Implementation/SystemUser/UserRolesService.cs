using AutoMapper;

using PTSL.Ovidhan.Business.Businesses.Interface;
using PTSL.Ovidhan.Service.Services.Interface;

namespace PTSL.Ovidhan.Service.Services.Implementation;

public class UserRolesService : IUserRolesService
{
    public readonly IUserRolesBusiness _userRolesBusiness;
    public IMapper _mapper;

    public UserRolesService(IUserRolesBusiness userRolesBusiness, IMapper mapper)
    {
        _userRolesBusiness = userRolesBusiness;
        _mapper = mapper;
    }
}
