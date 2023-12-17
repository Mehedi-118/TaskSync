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
	public class AccesslistController : Controller
	{
		private readonly IAccesslistService _AccesslistService;
		private readonly IModuleService _ModuleService;

		public AccesslistController(HttpHelper httpHelper)
		{
			_AccesslistService = new AccesslistService(httpHelper);
			_ModuleService = new ModuleService(httpHelper);
		}
		// GET: Accesslist
		public ActionResult Index()
		{
			(ExecutionState executionState, List<AccesslistVM> entity, string message) returnResponse = _AccesslistService.List();
			return View(returnResponse.entity);
		}

		// GET: Accesslist/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return Ok();
			}
			(ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.GetById(id);
			return View(returnResponse.entity);
		}

		// GET: Accesslist/Create
		public ActionResult Create()
		{
			AccesslistVM entity = new AccesslistVM();
			(ExecutionState executionState, List<ModuleVM> entity, string message) returnResponse = _ModuleService.List();

			if (returnResponse.entity != null)
			{
				ViewBag.BaseModule = new SelectList(returnResponse.entity, "Id", "ModuleName");
			}
			else
			{
				ViewBag.BaseModule = new SelectList("");
			}

			return View(entity);
		}

		// POST: Accesslist/Create
		[HttpPost]
		public ActionResult Create(AccesslistVM entity)
		{
			try
			{
				if (ModelState.IsValid)
				{
					entity.IsActive = true;
					entity.CreatedAt = DateTime.Now;

					(ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.Create(entity);

////					//Session["Message"] = returnResponse.message;
//					//Session["executionState"] = returnResponse.executionState;

					if (returnResponse.executionState.ToString() != "Created")
					{
						(ExecutionState executionState, List<ModuleVM> entity, string message) ModuleLists = _ModuleService.List();

						ViewBag.BaseModule = new SelectList(ModuleLists.entity, "Id", "ModuleName", entity.BaseModule);
						return View(entity);
					}
					else
					{
						return RedirectToAction("Index");
					}
				}
				return View();
			}
			catch
			{
				return View();
			}
		}

		// GET: Accesslist/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return Ok();
			}
			(ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.GetById(id);
			(ExecutionState executionState, List<ModuleVM> entity, string message) ModuleLists = _ModuleService.List();

			ViewBag.BaseModule = new SelectList(ModuleLists.entity, "Id", "ModuleName", returnResponse.entity.BaseModule);

			return View(returnResponse.entity);
		}

		// POST: Accesslist/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, AccesslistVM entity)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (id != entity.Id)
					{
						return RedirectToAction(nameof(AccesslistController.Index), "Accesslist");
					}
					entity.IsActive = true;
					entity.IsDeleted = false;
					entity.UpdatedAt = DateTime.Now;
					(ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.Update(entity);
////					//Session["Message"] = returnResponse.message;
//					//Session["executionState"] = returnResponse.executionState;
					if (returnResponse.executionState.ToString() != "Updated")
					{
						(ExecutionState executionState, List<ModuleVM> entity, string message) ModuleLists = _ModuleService.List();
						ViewBag.BaseModule = new SelectList(ModuleLists.entity, "Id", "ModuleName", returnResponse.entity.BaseModule);
						return View(entity);
					}
					else
					{
						return RedirectToAction("Index");
					}
				}
				return View();
			}
			catch
			{
				return View();
			}
		}

		// GET: Accesslist/Delete/5
		public JsonResult Delete(int id)
		{
			(ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.Delete(id);
			if (returnResponse.executionState.ToString() == "Updated")
			{
				returnResponse.message = "Accesslist deleted successfully.";
			}
			return Json(new { Message = returnResponse.message, returnResponse.executionState }, SerializerOption.Default);
			//return View();
		}

		// POST: Accesslist/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, AccesslistVM entity)
		{
			try
			{
				if (id != entity.Id)
				{
					return RedirectToAction(nameof(AccesslistController.Index), "Accesslist");
				}
				//entity.IsActive = true;
				entity.IsDeleted = true;
				entity.UpdatedAt = DateTime.Now;
				(ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.Update(entity);
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
