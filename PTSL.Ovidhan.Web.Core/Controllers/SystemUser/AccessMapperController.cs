using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PTSL.Ovidhan.Web.Core.Helper;
using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.Ovidhan.Web.Core.Services.Implementation.SystemUser;
using PTSL.Ovidhan.Web.Core.Services.Interface.SystemUser;
using PTSL.Ovidhan.Web.Helper;

namespace PTSL.Ovidhan.Web.Core.Controllers.SystemUser
{
	[SessionAuthorize]
	public class AccessMapperController : Controller
	{
		private readonly IAccessMapperService _AccessMapperService;
		private readonly IModuleService _ModuleService;
		private readonly IAccesslistService _AccesslistService;
		private readonly IPmsGroupService _PmsGroupService;
		//public AccessMapperController(): this(new AccessMapperService())
		//{
		//}
		public AccessMapperController(HttpHelper httpHelper)
		{
			_AccessMapperService = new AccessMapperService(httpHelper);
			_ModuleService = new ModuleService(httpHelper);
			_AccesslistService = new AccesslistService(httpHelper);
			_PmsGroupService = new PmsGroupService(httpHelper);
		}
		// GET: AccessMapper
		//public ActionResult Index()
		//{
		//    (ExecutionState executionState, List<AccessMapperVM> entity, string message) returnResponse = _AccessMapperService.List();
		//     return View(returnResponse.entity);
		//}
		public ActionResult Index(int Mid = 0)
		{
			ViewBag.PageTitle = "ACCESS MAPPER";

			List<AccessMapperViewModelWithList> accessMapper = new List<AccessMapperViewModelWithList>();
			AccessMapperViewModelWithList accessMapperViewModelWithList = new AccessMapperViewModelWithList();

			//var payloadResponse = repository.GlobalApiCallFunction(null, "AccessMapperList");
			accessMapper = AccessMapperList();

			ViewBag.AccessMapperList = new SelectList(accessMapper, "MapperID", "MapperName");

			if (Mid != 0)
			{
				accessMapper = accessMapper.Where(a => a.MapperID == Mid).ToList();
				accessMapperViewModelWithList = accessMapper.Where(a => a.MapperID != Mid).FirstOrDefault();
			}

			if (Mid == 0)
			{

				accessMapperViewModelWithList = accessMapper.Where(a => a.MapperID != 0).FirstOrDefault();
			}
			return View(accessMapper);

		}

		public List<AccessMapperViewModelWithList> AccessMapperList()
		{
			//List<AccessMapperVM> accessMapper = _AccessMapperService.GetAll().Where(x => x.IsRemoved == false).ToList();
			(ExecutionState executionState, List<AccessMapperVM> entity, string message) returnResponse = _AccessMapperService.List();

			List<AccessMapperViewModelWithList> accessMapperViewModelList = new List<AccessMapperViewModelWithList>();
			if (returnResponse.entity != null)
			{
				foreach (var item in returnResponse.entity)
				{
					AccessMapperViewModelWithList accessMapperViewModel = new AccessMapperViewModelWithList();
					List<AccesslistVM> ListOfAccess = new List<AccesslistVM>();
					List<int> AccessListInt = new List<int>();
					string s = item.AccessList;
					string[] values = s.Split(',');
					foreach (var item2 in values)
					{
						AccessListInt.Add(Convert.ToInt32(item2));
					}
					foreach (var item3 in AccessListInt)
					{
						(ExecutionState executionState, AccesslistVM entity, string message) returnAccesslistResponse = _AccesslistService.GetById(item3);

						//AccesslistVM accesslist = _AccesslistService.GetById(item3);
						if (returnAccesslistResponse.entity != null)
						{
							if (returnAccesslistResponse.entity.AccessStatus != 1 || returnAccesslistResponse.entity.IsVisible != 1)
							{
								ListOfAccess.Add(returnAccesslistResponse.entity);
							}
						}

					}

					(ExecutionState executionState, PmsGroupVM entity, string message) returnPmsGroupResponse = _PmsGroupService.GetById(item.Id);

					//PmsGroupVM pmsGroup = _PmsGroupService.GetById(item.MapperID);
					if (returnPmsGroupResponse.entity != null)
					{
						accessMapperViewModel.MapperID = item.Id;
						accessMapperViewModel.MapperName = returnPmsGroupResponse.entity.GroupName;
						accessMapperViewModel.AccessList = ListOfAccess;
						accessMapperViewModel.CreateDate = item.CreatedAt;
						//accessMapperViewModel.CreateTime = item.CreatedAt.ToShortTimeString();

						accessMapperViewModelList.Add(accessMapperViewModel);
					}

				}
			}
			return accessMapperViewModelList;
		}

		public List<PmsGroupViewModel> GetGroupDDLForCreate()
		{
			(ExecutionState executionState, List<PmsGroupVM> entity, string message) returnPmsGroupResponse = _PmsGroupService.List();

			var accessList = returnPmsGroupResponse.entity
			.Select(g => new PmsGroupViewModel
			{
				Id = g.Id,
				GroupName = g.GroupName

			}).ToList();

			(ExecutionState executionState, List<AccessMapperVM> entity, string message) returnAccessMapperResponse = _AccessMapperService.List();

			//var list = returnAccessMapperResponse.entity.Where(x => x.IsRemoved == false).ToList();
			foreach (var item in returnAccessMapperResponse.entity)
			{
				accessList.RemoveAll(r => r.Id == item.Id);
			}

			return accessList;
		}

		public JsonResult AccessMapperDropDown(int Mid)
		{
			try
			{
				AccessMapperVM accessMapper = new AccessMapperVM();
				accessMapper.Id = Mid;
				//var payloadResponse = repository.GlobalApiCallFunction(accessMapper, "AccessListById");
				List<AccessMapperViewModelWithList> result = AccessListById(accessMapper);

				if (Mid != 0)
				{
					//accessMapper = accessMapper.Where(a => a.MapperID == Mid).ToList();
					// accessMapperViewModelWithList = accessMapper.Where(a => a.MapperID != Mid).FirstOrDefault();
				}

				return Json(result, SerializerOption.Default);

			}
			catch (Exception)
			{
				return Json(null);
			}
		}

		public JsonResult AccessListById(int id)
		{
			PmsGroupVM pmsGroup = new PmsGroupVM();
			pmsGroup.Id = id;
			var GroupList = AccessMapperListById(pmsGroup);
			return Json(GroupList.AccessList, SerializerOption.Default);
		}

		public AccessMapperViewModelWithList AccessMapperListById(PmsGroupVM pms)
		{
			//AccessMapperVM accessMapper = _AccessMapperService.GetById(pms.Id);
			(ExecutionState executionState, AccessMapperVM entity, string message) returnAccessMapperResponse = _AccessMapperService.GetById(pms.Id);
			AccessMapperVM accessMapper = returnAccessMapperResponse.entity;
			AccessMapperViewModelWithList accessMapperViewModel = new AccessMapperViewModelWithList();
			AccessMapperViewModelWithList accessMapperViewModelList = new AccessMapperViewModelWithList();
			List<AccesslistVM> ListOfAccess = new List<AccesslistVM>();
			List<int> AccessListInt = new List<int>();
			if (returnAccessMapperResponse.entity != null)
			{
				string s = returnAccessMapperResponse.entity.AccessList;
				string[] values = s.Split(',');
				foreach (var item2 in values)
				{
					AccessListInt.Add(Convert.ToInt32(item2));
				}
				foreach (var item3 in AccessListInt)
				{
					(ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.GetById(item3);

					AccesslistVM accesslist = returnResponse.entity;
					//if (accesslist.IsVisible != 1 || accesslist.AccessStatus != 1)
					//{
					ListOfAccess.Add(accesslist);
					//}
				}
				(ExecutionState executionState, PmsGroupVM entity, string message) returnPmsGroupResponse = _PmsGroupService.GetById(accessMapper.Id);

				PmsGroupVM pmsGroup = returnPmsGroupResponse.entity;
				accessMapperViewModelList.MapperID = accessMapper.Id;
				accessMapperViewModelList.MapperName = pmsGroup.GroupName;
				accessMapperViewModelList.AccessList = ListOfAccess;
				accessMapperViewModelList.CreateDate = accessMapper.CreatedAt;
				//accessMapperViewModelList.CreateTime = accessMapper.CreateTime;
			}

			return accessMapperViewModelList;

		}
		public List<AccessMapperViewModelWithList> AccessListById(AccessMapperVM accessModel)
		{
			(ExecutionState executionState, AccessMapperVM entity, string message) returnAccessMapperResponse = _AccessMapperService.GetById(accessModel.Id);

			AccessMapperVM accessMapper = returnAccessMapperResponse.entity;

			List<AccessMapperViewModelWithList> accessMapperViewModelList = new List<AccessMapperViewModelWithList>();
			//foreach (var item in accessMapper)
			//{
			AccessMapperViewModelWithList accessMapperViewModel = new AccessMapperViewModelWithList();
			List<AccesslistVM> ListOfAccess = new List<AccesslistVM>();
			List<int> AccessListInt = new List<int>();
			string s = accessMapper.AccessList;
			string[] values = s.Split(',');
			foreach (var item2 in values)
			{
				AccessListInt.Add(Convert.ToInt32(item2));
			}
			foreach (var item3 in AccessListInt)
			{
				try
				{
					(ExecutionState executionState, AccesslistVM entity, string message) returnAccesslistResponse = _AccesslistService.GetById(item3);

					var accesslist = returnAccesslistResponse.entity;

                    if (accesslist is null) continue;

					if (accesslist.AccessStatus != 1 || accesslist.IsVisible != 1)
					{
						ListOfAccess.Add(accesslist);
					}
				}
				catch (Exception ex)
				{

					//return Request.CreateResponse(HttpStatusCode.OK, new PayloadResponse { Success = false, Message = ex.ToString() });
				}

			}
			(ExecutionState executionState, PmsGroupVM entity, string message) returnPmsGroupResponse = _PmsGroupService.GetById(accessMapper.Id);

			PmsGroupVM pmsGroup = returnPmsGroupResponse.entity;
			accessMapperViewModel.MapperID = accessMapper.Id;
			accessMapperViewModel.MapperName = pmsGroup.GroupName;
			accessMapperViewModel.AccessList = ListOfAccess;
			accessMapperViewModel.CreateDate = accessMapper.CreatedAt;
			//accessMapperViewModel.CreateTime = accessMapper.CreateTime;

			accessMapperViewModelList.Add(accessMapperViewModel);
			//}

			return accessMapperViewModelList;

		}

		public JsonResult AccessList()
		{
			List<AccesslistVM> GroupList = new List<AccesslistVM>();
			GroupList = GetAllAccessLists().OrderBy(a => a.BaseModule).ToList();
			//    var newList = GroupList.OrderBy(a => a.BaseModule).ToList();
			//    return Json(newList, SerializerOption.Default);
			//SHUVO Added
			return Json(GroupList, SerializerOption.Default);
		}

		public List<AccesslistVM> GetAllAccessLists()
		{
			List<AccesslistVM> accessList = new List<AccesslistVM>();
			(ExecutionState executionState, List<AccesslistVM> entity, string message) returnAccesslistResponse = _AccesslistService.List();
			accessList = returnAccesslistResponse.entity.Where(a => a.AccessStatus != 1).OrderByDescending(a => a.Id).ToList();

			List<AccesslistVM> accessModelList = new List<AccesslistVM>();
			foreach (var list in accessList)
			{
				AccesslistVM accesslistModel = new AccesslistVM();
				//var moudleName = _ModuleService.GetById(list.BaseModule);
				(ExecutionState executionState, ModuleVM entity, string message) returnPmsGroupResponse = _ModuleService.GetById(list.BaseModule);

				accesslistModel.ModuleName = returnPmsGroupResponse.entity.ModuleName;
				accesslistModel.Id = list.Id;
				accesslistModel.ActionName = list.ActionName;
				accesslistModel.BaseModule = list.BaseModule;
				accesslistModel.ControllerName = list.ControllerName;
				accesslistModel.Mask = list.Mask;

				accessModelList.Add(accesslistModel);
			}
			return accessModelList;
		}




		// GET: AccessMapper/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return Ok();
			}
			(ExecutionState executionState, AccessMapperVM entity, string message) returnResponse = _AccessMapperService.GetById(id);
			return View(returnResponse.entity);
		}

		// GET: AccessMapper/Create
		public ActionResult Create()
		{
			AccessMapperVM entity = new AccessMapperVM();
			var GroupList = GetGroupDDLForCreate();//JsonConvert.DeserializeObject<List<PmsGroupViewModel>>(payloadResponse.Payload.ToString());
			ViewBag.GroupList = GroupList;
			return View(entity);
		}

		[HttpPost]
		public JsonResult Create(AccessMapperVM entity)
		{
			try
			{
				// TODO: Add insert logic here

				if (ModelState.IsValid)
				{
					entity.RoleStatus = 0;
					entity.IsVisible = 0;
					entity.IsActive = true;
					entity.CreatedAt = DateTime.Now;
					(ExecutionState executionState, AccessMapperVM entity, string message) returnResponse = _AccessMapperService.Create(entity);

					if (returnResponse.executionState.ToString() == "Created")
					{
						returnResponse.message = "Access Mapper Created successfully.";
					}
					return Json(new { Message = returnResponse.message, returnResponse.executionState }, SerializerOption.Default);
				}
				return Json(new { Message = "Access Mapper Created faild.", executionState = 0 }, SerializerOption.Default);
			}
			catch
			{
				return Json(new { Message = "Access Mapper Created faild.", executionState = 0 }, SerializerOption.Default);
			}
		}



		// GET: AccessMapper/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return Ok();
			}
			(ExecutionState executionState, AccessMapperVM entity, string message) returnResponse = _AccessMapperService.GetById(id);
			var GroupList = GetGroupDDLForCreate();//null; JsonConvert.DeserializeObject<List<PmsGroupViewModel>>(payloadResponse.Payload.ToString());
			List<PmsGroupViewModel> list = GroupList;
			list = list.Where(a => a.Id == id).ToList();
			ViewBag.GroupList = list;
			ViewBag.Id = id;
			return View();
		}

		// POST: AccessMapper/Edit/5
		[HttpPost]
		public JsonResult Edit(AccessMapperVM accessMapperViewModel)
		{
			try
			{
				AccessMapperVM accessMapper = new AccessMapperVM();

				if (ModelState.IsValid)
				{
					accessMapper.AccessList = accessMapperViewModel.AccessList;
					accessMapper.CreatedAt = DateTime.Now;
					accessMapper.RoleStatus = 0;
					accessMapper.IsVisible = 0;
					accessMapper.IsActive = true;
					accessMapper.Id = accessMapperViewModel.Id;
					//bool isUpdate = UpdateNewAccessMapper(accessMapper);
					(ExecutionState executionState, AccessMapperVM entity, string message) returnResponse = _AccessMapperService.Update(accessMapper);

					//if (repository.GlobalApiCallFunction(accessMapper, "UpdateNewAccessMapper").Success)
					if (returnResponse.executionState.ToString() == "Updated")
					{
						returnResponse.message = "Access Mapper Updated successfully.";
					}
					return Json(new { Message = returnResponse.message, returnResponse.executionState }, SerializerOption.Default);


				}
				else
				{
					return Json(new { Message = "Access Mapper Update Faild", executionState = 0 }, SerializerOption.Default);
				}
			}
			catch (Exception)
			{
				return Json(new { Message = "Access Mapper Update Faild", executionState = 0 }, SerializerOption.Default);
			}
		}



		// GET: AccessMapper/Delete/5
		public JsonResult Delete(int id)
		{
			(ExecutionState executionState, AccessMapperVM entity, string message) returnResponse = _AccessMapperService.Delete(id);
			if (returnResponse.executionState.ToString() == "Updated")
			{
				returnResponse.message = "AccessMapper deleted successfully.";
			}
			return Json(new { Message = returnResponse.message, returnResponse.executionState }, SerializerOption.Default);
			//return View();
		}

		// POST: AccessMapper/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, AccessMapperVM entity)
		{
			try
			{
				// TODO: Add update logic here
				if (id != entity.Id)
				{
					return RedirectToAction(nameof(AccessMapperController.Index), "AccessMapper");
				}
				//entity.IsActive = true;
				entity.IsDeleted = true;
				entity.UpdatedAt = DateTime.Now;
				(ExecutionState executionState, AccessMapperVM entity, string message) returnResponse = _AccessMapperService.Update(entity);
////				//Session["Message"] = returnResponse.message;
//				//Session["executionState"] = returnResponse.executionState;
				//return View(returnResponse.entity);
				// return RedirectToAction("Edit?id="+id);
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
