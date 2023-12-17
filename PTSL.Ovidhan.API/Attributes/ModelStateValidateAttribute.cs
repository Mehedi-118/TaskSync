using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PTSL.Ovidhan.API.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class ModelStateValidateAttribute : ActionFilterAttribute
    {
        public ModelStateValidateAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (!context.ModelState.IsValid)
            {
                var test = context.ModelState;
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
