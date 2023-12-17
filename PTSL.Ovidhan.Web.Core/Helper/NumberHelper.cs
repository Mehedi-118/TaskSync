using System.Globalization;

namespace PTSL.Ovidhan.Web.Core.Helper;

public static class NumberHelper
{
    public static string ToBDTCurrency(this double number)
    {
        return number.ToString("C", new CultureInfo("bn-BD", false).NumberFormat);
    }
}
