using jrgnsn.net.Web.Client.Models;

namespace jrgnsn.net.Web.Client.Tests.Data;

public static partial class TestData
{
    public static Post TestPost = new()
    {
        Content = "Test content",
        Id = 1,
        PublishDate = new DateTime(2021, 1, 1),
        Slug = "test-post",
        Summary = "Test summary",
        Tags = new List<Tag>(),
        Title = "Test post"
    };
}
