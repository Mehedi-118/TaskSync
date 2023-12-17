using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PTSL.GENERIC.Service.Services.Output;
using PTSL.Ovidhan.Api.Controllers;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model;
using PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.Ovidhan.Service.Services.Interface.GeneralSetup;

namespace PTSL.Ovidhan.API.Controllers.GeneralSetup
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : BaseController<IReminderService, ReminderVM, Reminder>
    {
        private readonly IReminderService _service;

        public ReminderController(IReminderService service) :
            base(service)
        {
            _service = service;
        }
        [HttpPost("Create")]
        public override async Task<ActionResult<WebApiResponse<ReminderVM>>> CreateAsync(ReminderVM model)
        {
            WebApiResponse<ReminderVM> response = new WebApiResponse<ReminderVM>((ExecutionState.Failure, null!, "Failed to Create Task"));

            string? userId = HttpContext.User.FindFirst("UserId")?.Value;
            model.CreatedAt = DateTime.Now;
            model.CreatedBy = userId;

            (ExecutionState executionState, ReminderVM entity, string message) reminderResponse = await _service.CreateAsync(model);
            
            response.Data = reminderResponse.entity;
            response.ExecutionState = reminderResponse.executionState;
            response.Message = reminderResponse.message;
            return Ok(response);

        }

    }
}