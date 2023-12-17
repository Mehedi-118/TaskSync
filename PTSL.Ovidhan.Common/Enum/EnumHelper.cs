using PTSL.Ovidhan.Common.Model.BaseModels;

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace PTSL.Ovidhan.Common.Enum;

public static class EnumHelper
{
    public static string? GetEnumDisplayName(this System.Enum enumValue)
    {
        var name = enumValue.GetType()
          .GetMember(enumValue.ToString())
          .First()
          .GetCustomAttribute<DisplayAttribute>()
          ?.GetName();
        return name;
    }

    public static IEnumerable<DropdownVM> GetEnumDropdowns<T>() where T : System.Enum
    {
        var list = System.Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(x => new DropdownVM
            {
                Id = (string)(object)x,
                Name = x.GetEnumDisplayName() ?? x.ToString(),
            });

        return list;
    }
}
