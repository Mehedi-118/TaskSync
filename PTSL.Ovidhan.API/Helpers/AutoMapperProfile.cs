using AutoMapper;

using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Entity.Tasks;
using PTSL.Ovidhan.Common.Entity.UserEntitys;
using PTSL.Ovidhan.Common.Model;
using PTSL.Ovidhan.Common.Model.EntityViewModels;
using PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.Ovidhan.Common.Model.EntityViewModels.SystemUser;

namespace PTSL.Ovidhan.Api.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
       
        //User Manager
        base.CreateMap<User, UserVM>().ReverseMap();
        base.CreateMap<UserDropdownVM, User>().ReverseMap();
        base.CreateMap<UserVM, UserUpdateVM>().ReverseMap();
        base.CreateMap<LoginResultVM, User>().ReverseMap();
        base.CreateMap<Role, RoleVM>().ReverseMap();

        base.CreateMap<Category, CategoryVM>()
            .ForMember(dest => dest.TodoVMs, opt => opt.MapFrom(src => src.Todos)).ReverseMap();
        base.CreateMap<Todo, TodoVM>().ReverseMap();
        base.CreateMap<Reminder, ReminderVM>().ReverseMap();


    }
}
