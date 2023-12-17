using Microsoft.AspNetCore.Mvc;

using PTSL.Ovidhan.Web.Core.Helper;
using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.Ovidhan.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.Ovidhan.Web.Core.Services.Implementation.SystemUser;
using PTSL.Ovidhan.Web.Core.Services.Interface.GeneralSetup;
using PTSL.Ovidhan.Web.Core.Services.Interface.SystemUser;
using PTSL.Ovidhan.Web.Helper;
using PTSL.Ovidhan.Web.Services.Implementation.GeneralSetup;

namespace PTSL.Ovidhan.Web.Core.Controllers;

[SessionAuthorize]
public class HomeController : Controller
{
    private readonly IPmsGroupService _PmsGroupService;
    private readonly IAccessMapperService _AccessMapperService;
    private readonly IAccesslistService _AccesslistService;
    private readonly IModuleService _ModuleService;
    private readonly IUserService _UserService;

    private readonly IDivisionService _DivisionService;
    private readonly IDistrictService _DistrictService;
    private readonly IUpazillaService _UpazillaService;
    private readonly IUnionService _UnionService;

    public HomeController(HttpHelper httpHelper)
    {
        _PmsGroupService = new PmsGroupService(httpHelper);
        _AccessMapperService = new AccessMapperService(httpHelper);
        _AccesslistService = new AccesslistService(httpHelper);
        _ModuleService = new ModuleService(httpHelper);
        _UserService = new UserService(httpHelper);
        _DivisionService = new DivisionService(httpHelper);
        _DistrictService = new DistrictService(httpHelper);
        _UpazillaService = new UpazillaService(httpHelper);
        _UnionService = new UnionService(httpHelper);
    }

    public IActionResult Index()
    {

        return View();
    }

    public JsonResult RootMenue()
    {
        //IGXMenu menu = new IGXMenu();
        Menu menu = new Menu();

        List<MenueViewModel> menueList = new List<MenueViewModel>();

        try
        {
            menu = PmsRootMenueList();
            menueList = menu.MenuList;
            return Json(menu, SerializerOption.Default); //ViewBag.GroupList = GroupList.ToList();
        }
        catch (Exception ex)
        {
            return Json("");
        }

    }

    public Menu PmsRootMenueList()
    {
        Menu menu = new Menu();
        try
        {

            List<CustomerAccessList> AllAccesslist = new List<CustomerAccessList>();

            List<MenueViewModel> menueList = new List<MenueViewModel>();

            List<int> AccesList = new List<int>();
            //var aspNetUser = UserManager.Users.ToList().Where(x => !x.IsRemoved && x.Id == User.Identity.GetUserId()).FirstOrDefault();
            long GroupID = 1;// Convert.ToInt32(aspNetUser.PmsGroupID);


            long.TryParse(HttpContext.Session.GetString("UserId"), out var LoggedInUser); // User.Identity.GetUserId();

            var loginUser = _UserService.GetById(LoggedInUser);
            var UserName = HttpContext.Session.GetString("UserEmail");// User.Identity.GetUserName();
            // var PmsGroupId = _PmsGroupService.GetById(GroupID);
            if (loginUser.entity != null)
            {
                GroupID = loginUser.entity.PmsGroupID;
            }
            (ExecutionState executionState, PmsGroupVM entity, string message) returnPmsGroupResponse = _PmsGroupService.GetById(GroupID);

            string GroupName = returnPmsGroupResponse.entity.GroupName;


            if (GroupID != 0)
            {
                (ExecutionState executionState, AccessMapperVM entity, string message) returnAccessMapperVMResponse = _AccessMapperService.GetById(GroupID);

                //var accessMapper = _AccessMapperService.GetById(GroupID);
                string s = string.Empty;
                if (returnAccessMapperVMResponse.entity != null && returnAccessMapperVMResponse.entity.AccessList != null)
                {
                    s = returnAccessMapperVMResponse.entity.AccessList;
                }
                string[] values = s.Split(',');
                foreach (var value in values)
                {
                    AccesList.Add(Convert.ToInt32(value));
                }
                foreach (var item in AccesList)
                {
                    try
                    {
                        //var access = _AccesslistService.GetById(item);
                        (ExecutionState executionState, AccesslistVM entity, string message) returnAccesslistResponse = _AccesslistService.GetById(item);

                        CustomerAccessList custom = new CustomerAccessList();

                        if (returnAccesslistResponse.entity != null && returnAccesslistResponse.entity.IsVisible == 0)
                        {
                            custom.AccessID = returnAccesslistResponse.entity.Id;
                            custom.AccessStatus = returnAccesslistResponse.entity.AccessStatus;
                            custom.ActionName = returnAccesslistResponse.entity.ActionName;
                            custom.BaseModule = returnAccesslistResponse.entity.BaseModule;
                            custom.ControllerName = returnAccesslistResponse.entity.ControllerName;
                            custom.IconClass = returnAccesslistResponse.entity.IconClass;
                            custom.Mask = returnAccesslistResponse.entity.Mask;
                            custom.BaseModuleIndex = returnAccesslistResponse.entity.BaseModuleIndex;
                            AllAccesslist.Add(custom);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                (ExecutionState executionState, List<ModuleVM> entity, string message) returnModuleVMResponse = _ModuleService.List();

                var ModuleList = returnModuleVMResponse.entity.OrderBy(a => a.MenueOrder).ToList();
                foreach (var item in ModuleList)
                {
                    MenueViewModel menue = new MenueViewModel();
                    menue.ModuleID = item.Id;
                    menue.ModuleName = item.ModuleName;
                    menue.ModuleIcon = item.ModuleIcon;
                    var module = _ModuleService.GetById(item.Id);

                    if (module.entity.IsVisible == 0)
                    {
                        var menulist = AllAccesslist.Where(a => a.BaseModule == item.Id).OrderBy(a => a.BaseModuleIndex).ToList();
                        if (menulist.Count > 0)
                        {
                            menue.AccessList = menulist;
                            menueList.Add(menue);
                        }
                    }
                }
            }
            menu.MenuList = menueList;
            menu.UserName = UserName;
            menu.GroupName = GroupName;

            return menu;

        }
        catch (Exception ex)
        {
            return menu;
        }
    }
}
