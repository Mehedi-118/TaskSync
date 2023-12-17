using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model;
using PTSL.Ovidhan.Common.Model.BaseModels;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.Service.BaseServices;
using PTSL.Ovidhan.Common.Implementation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PTSL.Ovidhan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BaseController<TService, TModel, TEntity> : ControllerBase
        where TModel : BaseModel, new()
        where TEntity : BaseEntity, new()
        where TService : IBaseService<TModel, TEntity>

    {
        private TService Service { get; }
        public BaseController(TService service)
        {
            Service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<WebApiResponse<TModel>>> CreateAsync([FromBody] TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userIdJwt = HttpContext.User.FindFirst("UserId")?.Value;

            model.CreatedBy = userIdJwt;
            model.CreatedAt = DateTime.Now;

            (ExecutionState executionState, TModel entity, string message) result = await Service.CreateAsync(model);

            WebApiResponse<TModel> apiResponse = new WebApiResponse<TModel>(result);

            if (result.executionState == ExecutionState.Failure)
            {
                return Ok(apiResponse);
            }
            else
            {
                return Ok(apiResponse);
            }
        }

        [HttpGet("{id}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<WebApiResponse<TModel>>> GetAsync(long id)
        {
            //if (id == 0)
            //{
            //    return BadRequest();
            //}

            (ExecutionState executionState, TModel entity, string message) result = await Service.GetAsync(id);

            WebApiResponse<TModel> apiResponse = new WebApiResponse<TModel>(result);

            if (result.executionState == ExecutionState.Failure)
            {
                return Ok(apiResponse);
            }
            else
            {
                return Ok(apiResponse);
            }
        }

        [HttpGet("List")]
        public virtual async Task<ActionResult<WebApiResponse<IList<TModel>>>> List()
        {
            (ExecutionState executionState, IList<TModel> entity, string message) result = await Service.List();

            WebApiResponse<IList<TModel>> apiResponse = new WebApiResponse<IList<TModel>>(result);

            if (result.executionState == ExecutionState.Failure)
            {
                return Ok(apiResponse);
            }
            else
            {
                return Ok(apiResponse);
            }
        }            
        
        [HttpGet("Page")]
        public virtual async Task<ActionResult<WebApiResponse<PagedData<TModel>>>> Page([FromQuery] Pagination pagination)
        {
            (ExecutionState executionState, PagedData<TModel> data, string message) result = await Service.Page(pagination);

            WebApiResponse<PagedData<TModel>> apiResponse = new WebApiResponse<PagedData<TModel>>(result);

            if (result.executionState == ExecutionState.Failure)
            {
                return Ok(apiResponse);
            }
            else
            {
                return Ok(apiResponse);
            }
        }

        [HttpGet("DoesExist/{id}")]
        public virtual async Task<ActionResult<WebApiResponse<bool>>> DoesExist(long id)
        {
            (ExecutionState executionState, string message) result = await Service.DoesExistAsync(id);

            WebApiResponse<TModel> apiResponse = new WebApiResponse<TModel>(result);

            //if (result.executionState == ExecutionState.Failure)
            //{
            //    apiResponse.Data = false;
            //    return Ok(apiResponse);
            //}
            //else
            //{
            //    apiResponse.Data = true;
            //    return Ok(apiResponse);
            //}
            if (result.executionState == ExecutionState.Failure)
            {
                //apiResponse.Data = false;
                return Ok(apiResponse);
            }
            else
            {
                //apiResponse.Data = true;
                return Ok(apiResponse);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<WebApiResponse<TModel>>> UpdateAsync([FromBody] TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userIdJwt = HttpContext.User.FindFirst("UserId")?.Value;

            model.ModifiedBy = userIdJwt;
            model.UpdatedAt = DateTime.Now;

            (ExecutionState executionState, TModel entity, string message) result = await Service.UpdateAsync(model);
            WebApiResponse<TModel> apiResponse = new WebApiResponse<TModel>(result);

            if (result.executionState == ExecutionState.Failure)
            {
                return Ok(apiResponse);
            }
            else
            {
                return Ok(apiResponse);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<WebApiResponse<TModel>>> RemoveAsync(long id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            //var userid = Request.Headers["UserId"];
            //if (userid.Count > 0)
            //{
            //    model.CreatedBy = Convert.ToInt64(userid);
            //}
            (ExecutionState executionState, TModel entity, string message) result = await Service.RemoveAsync(id);
            WebApiResponse<TModel> apiResponse = new WebApiResponse<TModel>(result);

            if (result.executionState == ExecutionState.Failure)
            {
                return Ok(apiResponse);
            }
            else
            {
                return Ok(apiResponse);
            }
        }


        [HttpPut("MarkAsActive/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<TModel>>> MarkAsActiveAsync(long id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            (ExecutionState executionState, TModel entity, string message) result = await Service.MarkAsActiveAsync(id);
            WebApiResponse<TModel> apiResponse = new WebApiResponse<TModel>(result);

            if (result.executionState == ExecutionState.Failure)
            {
                return Ok(apiResponse);
            }
            else
            {
                return Ok(apiResponse);
            }
        }

        [HttpPut("MarkAsInactive/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<TModel>>> MarkAsInactiveAsync(long id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            (ExecutionState executionState, TModel entity, string message) result = await Service.MarkAsInactiveAsync(id);
            WebApiResponse<TModel> apiResponse = new WebApiResponse<TModel>(result);

            if (result.executionState == ExecutionState.Failure)
            {
                return Ok(apiResponse);
            }
            else
            {
                return Ok(apiResponse);
            }
        }

        [HttpPut("SoftDelete/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<bool>>> SoftDeleteAsync(long id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var userIdJwt = HttpContext.User.FindFirst("UserId")?.Value;

            (ExecutionState executionState, bool isDeleted, string message) result = await Service.SoftDeleteAsync(id, userIdJwt);
            var apiResponse = new WebApiResponse<bool>(result);

            if (result.executionState == ExecutionState.Failure)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Ok(apiResponse);
            }
            else
            {
                return Ok(apiResponse);
            }
        }
    }
}
