using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace PTSL.Ovidhan.Business
{
    public static class Helper
    {
        public static string GetEnumDisplayName(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Name;
            else
                return value.ToString();
        }
    }
}

//for call any of controller
//int value = 3;
//var Status = Helper.GetEnumDisplayName((NatureOfPromotion)3);