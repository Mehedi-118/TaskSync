using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;

using PTSL.Ovidhan.Api.Controllers;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
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
    public class CategoryController : BaseController<ICategoryService, CategoryVM, Category>
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service) :
            base(service)
        {
            _categoryService = service;
        }

        [HttpPost("Create")]
        public override Task<ActionResult<WebApiResponse<CategoryVM>>> CreateAsync([FromBody] CategoryVM model)
        {
            string? userId = HttpContext.User.FindFirst("UserId")?.Value;
            model.CreatedAt=DateTime.Now;
            model.CreatedBy = userId;
            model.UserId=userId;
            return base.CreateAsync(model);
        }


        [HttpGet("List")]
        public override async Task<ActionResult<WebApiResponse<IList<CategoryVM>>>> List()
        {
            string? userId = HttpContext.User.FindFirst("UserId")?.Value;
            WebApiResponse<IList<CategoryVM>> response = new WebApiResponse<IList<CategoryVM>>((ExecutionState.Failure, new List<CategoryVM>(), "Category Not Found"));
            if (string.IsNullOrEmpty(userId))
            {
                return Ok(response);
            }

            var userIdJwt = HttpContext.User.FindFirst("UserId")?.Value;

            (ExecutionState executionState, IList<CategoryVM> entity, string message) getCategories = await _categoryService.List(userIdJwt);
            if (getCategories.executionState != ExecutionState.Retrieved || getCategories.entity == null)
            {


                List<CategoryVM> init = await InitCategories(userId);
                if (init.Count == 0)
                {
                    return Ok(response);
                }
                response.Data = init;
                response.ExecutionState = ExecutionState.Success;
                response.Message = "Initial Categories Created";
                return Ok(response);

            }
            else if(getCategories.executionState == ExecutionState.Retrieved)
            {
                var getLoggedUserCategories= getCategories.entity.FirstOrDefault(c => c.UserId == userId);
                if (getLoggedUserCategories == null)
                {
                    List<CategoryVM> init = await InitCategories(userId);
                    if (init.Count == 0)
                    {
                        return Ok(response);
                    }
                    response.Data = init;
                    response.ExecutionState = ExecutionState.Success;
                    response.Message = "Initial Categories Created";
                    return Ok(response);
                }
            }
            response.Data = getCategories.entity;
            response.ExecutionState = ExecutionState.Success;
            response.Message = getCategories.message;
            return Ok(response);

        }

        private async Task<List<CategoryVM>> InitCategories(string userId)
        {
            List<CategoryVM> categories = new List<CategoryVM>
            {
                new CategoryVM
                {
                    TitleEn="Personal",
                    TitleBn="",
                    DescriptionBn="",
                    DescriptionEn="Schedule your personal task",
                    UserId=userId,
                    CreatedAt=DateTime.Now,
                    CreatedBy=userId
                },
                new CategoryVM
                {
                    TitleEn="Shopping",
                    TitleBn="",
                    DescriptionBn="",
                    DescriptionEn="Schedule your Shopping task",
                    UserId=userId,
                    CreatedAt=DateTime.Now,
                    CreatedBy=userId
                },
                new CategoryVM
                {
                    TitleEn="Office",
                    TitleBn="",
                    DescriptionBn="",
                    DescriptionEn="Schedule your Office task",
                    UserId=userId,
                    CreatedAt=DateTime.Now,
                    CreatedBy=userId
                },
                new CategoryVM
                {
                    TitleEn="Miscellenious",
                    TitleBn="",
                    DescriptionBn="",
                    DescriptionEn="Schedule your Miscellenious task",
                    UserId=userId,
                    CreatedAt=DateTime.Now,
                    CreatedBy=userId
                }
            };
            (ExecutionState executionState, IList<CategoryVM> entity, string message) initCategories = await _categoryService.CreateAsync(categories);
            if (initCategories.executionState != ExecutionState.Created)
            {
                return new List<CategoryVM>();
            }
            return initCategories.entity.ToList();
        }
    }
}