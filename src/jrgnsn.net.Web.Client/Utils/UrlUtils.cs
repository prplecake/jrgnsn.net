using jrgnsn.net.Web.Client.Models;

namespace jrgnsn.net.Web.Client.Utils;

public static class UrlUtils
{
    internal static string GetPermalink(object obj)
    {
        return obj.GetType().Name switch
        {
            nameof(Post) =>
                $"/{((Post)obj).PublishDate.Year}/{((Post)obj).PublishDate.Month}/{((Post)obj).PublishDate.Day}/{((Post)obj).Slug}",
            _ => throw new NotImplementedException()
        };
    }
}
