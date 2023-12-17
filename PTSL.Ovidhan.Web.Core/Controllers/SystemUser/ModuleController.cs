using Microsoft.AspNetCore.Mvc;
using PTSL.Ovidhan.Web.Core.Helper;
using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.Ovidhan.Web.Core.Services.Implementation.SystemUser;
using PTSL.Ovidhan.Web.Core.Services.Interface.SystemUser;
using PTSL.Ovidhan.Web.Helper;

namespace PTSL.Ovidhan.Web.Core.Controllers.SystemUser
{
	[SessionAuthorize]
	public class ModuleController : Controller
	{
		private readonly IModuleService _ModuleService;
		//private readonly IDivisionService _DivisionService;
		//public ModuleController(): this(new ModuleService())
		//{
		//}
		public ModuleController(HttpHelper httpHelper)
		{
			_ModuleService = new ModuleService(httpHelper);
			//_DivisionService = new DivisionService();
		}
		// GET: Module
		public ActionResult Index()
		{
			(ExecutionState executionState, List<ModuleVM> entity, string message) returnResponse = _ModuleService.List();
			return View(returnResponse.entity);
		}

		// GET: Module/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return Ok();
			}
			(ExecutionState executionState, ModuleVM entity, string message) returnResponse = _ModuleService.GetById(id);
			return View(returnResponse.entity);
		}

		// GET: Module/Create
		public ActionResult Create()
		{
			ModuleVM entity = new ModuleVM();
			return View(entity);
		}

		// POST: Module/Create
		[HttpPost]
		public ActionResult Create(ModuleVM entity)
		{
			try
			{
				if (ModelState.IsValid)
				{
					entity.IsActive = true;
					entity.CreatedAt = DateTime.Now;
					// TODO: Add insert logic here
					(ExecutionState executionState, ModuleVM entity, string message) returnResponse = _ModuleService.Create(entity);

////					//Session["Message"] = returnResponse.message;
//					//Session["executionState"] = returnResponse.executionState;

					if (returnResponse.executionState.ToString() != "Created")
					{
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


		// GET: Module/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return Ok();
			}
			(ExecutionState executionState, ModuleVM entity, string message) returnResponse = _ModuleService.GetById(id);

			return View(returnResponse.entity);
		}

		// POST: Module/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, ModuleVM entity)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// TODO: Add update logic here
					if (id != entity.Id)
					{
						return RedirectToAction(nameof(ModuleController.Index), "Module");
					}
					entity.IsActive = true;
					entity.IsDeleted = false;
					entity.UpdatedAt = DateTime.Now;
					(ExecutionState executionState, ModuleVM entity, string message) returnResponse = _ModuleService.Update(entity);
////					//Session["Message"] = returnResponse.message;
//					//Session["executionState"] = returnResponse.executionState;
					if (returnResponse.executionState.ToString() != "Updated")
					{
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

		// GET: Module/Delete/5
		public JsonResult Delete(int id)
		{
			(ExecutionState executionState, ModuleVM entity, string message) returnResponse = _ModuleService.Delete(id);
			if (returnResponse.executionState.ToString() == "Updated")
			{
				returnResponse.message = "Module deleted successfully.";
			}
			return Json(new { Message = returnResponse.message, returnResponse.executionState }, SerializerOption.Default);
			//return View();
		}

		// POST: Module/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, ModuleVM entity)
		{
			try
			{
				// TODO: Add update logic here
				if (id != entity.Id)
				{
					return RedirectToAction(nameof(ModuleController.Index), "Module");
				}
				//entity.IsActive = true;
				entity.IsDeleted = true;
				entity.UpdatedAt = DateTime.Now;
				(ExecutionState executionState, ModuleVM entity, string message) returnResponse = _ModuleService.Update(entity);
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
