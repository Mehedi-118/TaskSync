namespace PTSL.Ovidhan.Web.Core.Helper;

public static class DateTimeHelper
{
    public static string ToUIDateString(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd");
    }

    public static string ToUIDateStringNullable(this DateTime? dateTime)
    {
        return dateTime is not DateTime validDateTime ? string.Empty : validDateTime.ToString("yyyy-MM-dd");
    }

    public static string ToUITimeString(this DateTime dateTime)
    {
        return dateTime.ToString("hh:mm tt");
    }

    public static string ToUITimeStringNullable(this DateTime? dateTime)
    {
        return dateTime is not DateTime validDateTime ? string.Empty : validDateTime.ToString("yyyy-MM-dd");
    }
}

