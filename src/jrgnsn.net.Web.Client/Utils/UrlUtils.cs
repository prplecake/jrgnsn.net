using jrgnsn.net.Core.Entities;
using Post = jrgnsn.net.Web.Client.Models.Post;

namespace jrgnsn.net.Web.Client.Utils;

public static class UrlUtils
{
    internal static string GetPermalink(object obj)
    {
        return obj.GetType().Name switch
        {
            nameof(Post) =>
                $"/{((Post)obj).PublishDate.Year}/{((Post)obj).PublishDate.Month}/{((Post)obj).PublishDate.Day}/{((Post)obj).Slug}",
            nameof(TravelLog) =>
                $"/travel/{((TravelLog)obj).StartDate.ToString("yyyy")}-{((TravelLog)obj).StartDate.ToString("MM")}-{((TravelLog)obj).StartDate.ToString("dd")}-{((TravelLog)obj).Slug}",
            _ => throw new NotImplementedException()
        };
    }
}
