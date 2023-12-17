using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PTSL.Ovidhan.Web.Core.Helper;

public static class ModelStateExtensions
{
    public static string FirstErrorMessage(this ModelStateDictionary modelState)
    {
        return modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault() ?? string.Empty;
    }
}
