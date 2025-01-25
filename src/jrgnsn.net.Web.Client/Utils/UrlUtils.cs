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
                $"/{((Post)obj).PublishDate:yyyy}/{((Post)obj).PublishDate:MM}/{((Post)obj).PublishDate:dd}/{((Post)obj).Slug}",
            nameof(TravelLog) =>
                $"/travel/{((TravelLog)obj).StartDate:yyyy}-{((TravelLog)obj).StartDate:MM}-{((TravelLog)obj).StartDate:dd}-{((TravelLog)obj).Slug}",
            _ => throw new NotImplementedException()
        };
    }
}
