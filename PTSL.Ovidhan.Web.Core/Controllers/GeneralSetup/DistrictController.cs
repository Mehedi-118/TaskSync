using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PTSL.Ovidhan.Web.Core.Helper;
using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.GeneralSetup;
using PTSL.Ovidhan.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.Ovidhan.Web.Core.Services.Interface.GeneralSetup;
using PTSL.Ovidhan.Web.Helper;

namespace PTSL.Ovidhan.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class DistrictController : Controller
    {
        private readonly IDistrictService _DistrictService;
        private readonly IDivisionService _DivisionService;

        public DistrictController(HttpHelper httpHelper)
        {
            _DistrictService = new DistrictService(httpHelper);
            _DivisionService = new DivisionService(httpHelper);
        }
        // GET: District
        public ActionResult Index()
        {
            (ExecutionState executionState, List<DistrictVM> entity, string message) returnResponse = _DistrictService.List();
            return View(returnResponse.entity);
        }

        // GET: District/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Ok();
            }
            (ExecutionState executionState, DistrictVM entity, string message) returnResponse = _DistrictService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: District/Create
        public ActionResult Create()
        {
            DistrictVM entity = new DistrictVM();
            (ExecutionState executionState, List<DivisionVM> entity, string message) returnResponse = _DivisionService.List();
            ViewBag.DivisionId = new SelectList(returnResponse.entity, "Id", "Name");
            return View(entity);
        }

        // POST: District/Create
        [HttpPost]
        public ActionResult Create(DistrictVM entity)
        {
            try
            {
                (ExecutionState executionState, List<DivisionVM> entity, string message) returnResponse = _DivisionService.List();
                ViewBag.DivisionId = new SelectList(returnResponse.entity, "Id", "Name");
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, DistrictVM entity, string message) returnResponse1 = _DistrictService.Create(entity);

//                    Session["Message"] = returnResponse1.message;
//                    Session["executionState"] = returnResponse1.executionState;

                    if (returnResponse1.executionState.ToString() != "Created")
                    {
                        (ExecutionState executionState, List<DivisionVM> entity, string message) divisionLists = _DivisionService.List();

                        ViewBag.DivisionId = new SelectList(divisionLists.entity, "Id", "Name", entity.DivisionId);
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }

//                Session["Message"] = _ModelStateValidation.ModelStateErrorMessage(ModelState);
//                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
            catch
            {
//                Session["Message"] = "Form Data Not Valid.";
//                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
        }

        // GET: District/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return Ok();
            }
            (ExecutionState executionState, DistrictVM entity, string message) returnResponse = _DistrictService.GetById(id);
            (ExecutionState executionState, List<DivisionVM> entity, string message) divisionLists = _DivisionService.List();

            ViewBag.DivisionId = new SelectList(divisionLists.entity, "Id", "Name", returnResponse.entity.DivisionId);

            return View(returnResponse.entity);
        }

        // POST: District/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DistrictVM entity)
        {
            try
            {
                (ExecutionState executionState, List<DivisionVM> entity, string message) divisionLists = _DivisionService.List();
                ViewBag.DivisionId = new SelectList(divisionLists.entity, "Id", "Name", entity.DivisionId);
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(DistrictController.Index), "District");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, DistrictVM entity, string message) returnResponse = _DistrictService.Update(entity);
//                    Session["Message"] = returnResponse.message;
//                    Session["executionState"] = returnResponse.executionState;
                    if (returnResponse.executionState.ToString() != "Updated")
                    {
                        ViewBag.DivisionId = new SelectList(divisionLists.entity, "Id", "Name", returnResponse.entity.DivisionId);
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
//                Session["Message"] = _ModelStateValidation.ModelStateErrorMessage(ModelState);
//                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
            catch
            {
//                Session["Message"] = "Form Data Not Valid.";
//                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
        }

        // GET: District/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _DistrictService.DoesExist(id);
            string message = "Failed, You can't delete this item.";

            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, CheckDataExistOrNot.executionState }, SerializerOption.Default);
            }

            (ExecutionState executionState, DistrictVM entity, string message) returnResponse = _DistrictService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "Item deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }

            return Json(new { Message = returnResponse.message, returnResponse.executionState }, SerializerOption.Default);
        }

        // POST: District/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DistrictVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(DistrictController.Index), "District");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, DistrictVM entity, string message) returnResponse = _DistrictService.Update(entity);
//                Session["Message"] = returnResponse.message;
//                Session["executionState"] = returnResponse.executionState;
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
