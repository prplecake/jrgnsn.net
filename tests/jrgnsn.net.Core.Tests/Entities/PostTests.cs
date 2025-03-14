using jrgnsn.net.Core.Tests.Data;

namespace jrgnsn.net.Core.Tests.Entities;

[TestClass]
public class PostTests
{
    [TestMethod]
    public void Post_HasProperties()
    {
        var obj = TestData.TestPost;

        // Assert
        Assert.AreEqual(6, obj.PropertyCount());
        Assert.IsTrue(obj.HasProperty("Content"));
        Assert.IsTrue(obj.HasProperty("Id"));
        Assert.IsTrue(obj.HasProperty("PublishDate"));
        Assert.IsTrue(obj.HasProperty("Slug"));
        Assert.IsTrue(obj.HasProperty("Tags"));
        Assert.IsTrue(obj.HasProperty("Title"));
    }
    [TestMethod]
    public void TestPost_HasData()
    {
        var post = TestData.TestPost;

        // Assert
        Assert.IsNotNull(post);
        Assert.AreEqual("Test Content", post.Content);
        Assert.AreEqual(1, post.Id);
        Assert.AreEqual("Test Post", post.Title);
        Assert.AreEqual("test-post", post.Slug);
        Assert.IsNotNull(post.Tags);
        Assert.AreEqual(0, post.Tags.Count);
        Assert.AreEqual(DateTime.Parse("2025-01-01"), post.PublishDate);
    }
}
