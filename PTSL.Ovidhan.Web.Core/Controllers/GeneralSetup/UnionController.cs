using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PTSL.Ovidhan.Web.Core.Helper;
using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.GeneralSetup;
using PTSL.Ovidhan.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.Ovidhan.Web.Core.Services.Interface.GeneralSetup;
using PTSL.Ovidhan.Web.Helper;
using PTSL.Ovidhan.Web.Services.Implementation.GeneralSetup;

namespace PTSL.Ovidhan.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class UnionController : Controller
    {
        private readonly IUnionService _UnionService;
        private readonly IUpazillaService _UpazillaService;
        private readonly IDistrictService _DistrictService;
        private readonly IDivisionService _DivisionService;

        public UnionController(HttpHelper httpHelper)
        {
            _UnionService = new UnionService(httpHelper);
            _UpazillaService = new UpazillaService(httpHelper);
            _DistrictService = new DistrictService(httpHelper);
            _DivisionService = new DivisionService(httpHelper);
        }
        // GET: Union
        public ActionResult Index()
        {
            (ExecutionState executionState, List<UnionVM> entity, string message) returnResponse = _UnionService.List();
            return View(returnResponse.entity);
        }

        // GET: Union/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Ok();
            }
            (ExecutionState executionState, UnionVM entity, string message) returnResponse = _UnionService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: Union/Create
        public ActionResult Create()
        {
            UnionVM entity = new UnionVM();
            (ExecutionState executionState, List<DivisionVM> entity, string message) returnResponse = _DivisionService.List();

            ViewBag.DivisionId = new SelectList("");
            ViewBag.DistrictId = new SelectList("");
            ViewBag.UpazillaId = new SelectList("");

            if (returnResponse.entity != null)
            {
                ViewBag.DivisionId = new SelectList(returnResponse.entity, "Id", "Name");
            }
            return View(entity);
        }

        // POST: Union/Create
        [HttpPost]
        public ActionResult Create(UnionVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    (ExecutionState executionState, UnionVM entity, string message) returnResponse = _UnionService.Create(entity);
//                    Session["Message"] = returnResponse.message;
//                    Session["executionState"] = returnResponse.executionState;

                    if (returnResponse.executionState.ToString() != "Created")
                    {
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return Ok();
            }

            (ExecutionState executionState, UnionVM entity, string message) returnResponse = _UnionService.GetById(id);
            (ExecutionState executionState, List<DivisionVM> entity, string message) DivisionResponse = _DivisionService.List();

            ViewBag.DivisionId = new SelectList("");
            ViewBag.DistrictId = new SelectList("");
            ViewBag.UpazillaId = new SelectList("");

            if (returnResponse.entity != null)
            {
                var DivisionId = returnResponse.entity?.Upazilla?.District?.DivisionId ?? 0;
                var DistrictId = returnResponse.entity?.Upazilla?.DistrictId ?? 0;
                var UpazillaId = returnResponse.entity?.UpazillaId?? 0;

                if (DivisionResponse.entity != null)
                {
                    ViewBag.DivisionId = new SelectList(DivisionResponse.entity, "Id", "Name", DivisionId);
                }

                if (DivisionId != 0)
                {
                    (ExecutionState executionState, List<DistrictVM> entity, string message) DistrictResponse = _DistrictService.ListByDivision(DivisionId);

                    if (DistrictResponse.entity != null)
                    {
                        ViewBag.DistrictId = new SelectList(DistrictResponse.entity, "Id", "Name", DistrictId);
                    }
                }
                if (DistrictId != 0)
                {
                    (ExecutionState executionState, List<UpazillaVM> entity, string message) UpazillaResponse = _UpazillaService.ListByDistrict(DistrictId);

                    if (UpazillaResponse.entity != null)
                    {
                        ViewBag.UpazillaId = new SelectList(UpazillaResponse.entity, "Id", "Name", UpazillaId);
                    }
                }
            }

            return View(returnResponse.entity);
        }

        [HttpPost]
        public ActionResult Edit(int id, UnionVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(this.Index));
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, UnionVM entity, string message) returnResponse = _UnionService.Update(entity);
//                    Session["Message"] = returnResponse.message;
//                    Session["executionState"] = returnResponse.executionState;
                    if (returnResponse.executionState.ToString() != "Updated")
                    {
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

        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _UnionService.DoesExist(id);
            string message = "Failed, You can't delete this item.";

            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, CheckDataExistOrNot.executionState }, SerializerOption.Default);
            }

            (ExecutionState executionState, UnionVM entity, string message) returnResponse = _UnionService.Delete(id);
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

        [HttpPost]
        public ActionResult Delete(int id, UnionVM entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(this.Index));
                }
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, UnionVM entity, string message) returnResponse = _UnionService.Update(entity);
//                Session["Message"] = returnResponse.message;
//                Session["executionState"] = returnResponse.executionState;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
