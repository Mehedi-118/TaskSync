using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PTSL.Ovidhan.Api.Controllers;
using PTSL.Ovidhan.Common.Entity.Tasks;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model;
using PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.Service.Services.Interface.GeneralSetup;

namespace PTSL.Ovidhan.API.Controllers.GeneralSetup
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : BaseController<ITodoService, TodoVM, Todo>
    {
        private readonly ITodoService _service;
        private readonly IReminderService _reminderService;

        public TodoController(ITodoService service, IReminderService reminderService) :
            base(service)
        {
            _service = service;
            _reminderService = reminderService;
        }
        [HttpPost("Create")]
        public override async Task<ActionResult<WebApiResponse<TodoVM>>> CreateAsync([FromBody] TodoVM model)
        {
            WebApiResponse<TodoVM> response = new WebApiResponse<TodoVM>((ExecutionState.Failure, null!, "Failed to Create Task"));

            var userIdJwt = HttpContext.User.FindFirst("UserId")?.Value;

            model.CreatedBy = userIdJwt;
            model.CreatedAt = DateTime.Now;


            (ExecutionState executionState, TodoVM entity, string message) toDoCreateResponse = await _service.CreateAsync(model);
            if (toDoCreateResponse.executionState != ExecutionState.Created)
            {
                if (model.HasReminder)
                {
                    ReminderVM reminder = new ReminderVM
                    {
                        CreatedAt = model.CreatedAt,
                        CreatedBy = model.CreatedBy,
                        TodoId = toDoCreateResponse.entity.Id,
                        UserId = toDoCreateResponse.entity.UserId,
                        RemindAt = model.RemindAt,
                    };
                    (ExecutionState executionState, ReminderVM entity, string message) setReminder = await _reminderService.CreateAsync(reminder);
                    if (setReminder.executionState != ExecutionState.Created)
                    {
                        response.Message = $"{toDoCreateResponse.message} {setReminder.message}";
                    }
                    response.Message = $"{toDoCreateResponse.message}";

                }

            }
            response.Data = toDoCreateResponse.entity;
            response.ExecutionState = toDoCreateResponse.executionState;
            response.Message = toDoCreateResponse.message;
            return Ok(response);

        }


        [HttpGet("User/{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<TodoVM>>> GetAsync(string id)
        {
            (ExecutionState executionState, TodoVM entity, string message) result = await _service.GetTasksWithRemindersByUserId(id);

            WebApiResponse<TodoVM> apiResponse = new WebApiResponse<TodoVM>(result);

            if (result.executionState == ExecutionState.Failure)
            {
                return Ok(apiResponse);
            }
            else
            {
                return Ok(apiResponse);
            }
        }
        [HttpGet("{id}/Todos")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<TodoVM>>>> GetByCategoryIdAsync(long id)
        {
            (ExecutionState executionState, IList<TodoVM> entity, string message) result = await _service.GetByCategoryIdAsync(id);

            WebApiResponse<IList<TodoVM>> apiResponse = new WebApiResponse<IList<TodoVM>>(result);

            if (result.executionState == ExecutionState.Failure)
            {
                return Ok(apiResponse);
            }
            else
            {
                return Ok(apiResponse);
            }
        }

        [HttpGet("List/{userId}")]
        public async Task<ActionResult<WebApiResponse<IList<TodoVM>>>> List(string userId)
        {
            (ExecutionState executionState, IList<TodoVM> entity, string message) result = await _service.List(userId);

            WebApiResponse<IList<TodoVM>> apiResponse = new WebApiResponse<IList<TodoVM>>(result);

            if (result.executionState == ExecutionState.Failure)
            {
                return Ok(apiResponse);
            }
            else
            {
                return Ok(apiResponse);
            }
        }

    }
}