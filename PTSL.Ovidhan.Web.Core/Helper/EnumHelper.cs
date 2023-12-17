using PTSL.Ovidhan.Common.Model.BaseModels;
using PTSL.Ovidhan.Web.Core.Model.GeneralSetup;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PTSL.Ovidhan.Web.Core.Helper
{
    public static class EnumHelper
    {
        public static string GetEnumDisplayName(this System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Name;
            else
                return value.ToString();
        }

        public static IEnumerable<DropdownVM> GetEnumDropdowns<T>() where T : System.Enum
        {
            var list = System.Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(x => new DropdownVM
                {
                    Id = (string)(object)x,
                    Name = x.GetEnumDisplayName(),
                });

            return list;
        }
    }
}