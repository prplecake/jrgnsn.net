namespace jrgnsn.net.Core.Tests.Data;

partial class TestData
{
    public static readonly Post TestPost = new()
    {
        Content = "Test Content",
        Id = 1,
        PublishDate = DateTime.Parse("2025-01-01"),
        Slug = "test-post",
        Tags = new List<Tag>(),
        Title = "Test Post"
    };
}
