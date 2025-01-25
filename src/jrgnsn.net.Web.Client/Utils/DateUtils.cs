namespace jrgnsn.net.Web.Client.Utils;

public static class DateUtils
{
    public static string ToDateString(this DateTime date) => date.ToString("yyyy-MM-dd");
    public static string ToDateString(this DateOnly date) => date.ToString("yyyy-MM-dd");
}
