namespace jrgnsn.net.Core.Tests.Entities;

[TestClass]
public class TagTests
{
    Tag testTag = new()
    {
        Id = 1,
        Name = "Test",
        Slug = "test",
        Posts = []
    };
    [TestMethod]
    public void Tag_HasProperties()
    {
        var obj = testTag;

        // Assert
        Assert.AreEqual(4, obj.PropertyCount());
        Assert.IsTrue(obj.HasProperty("Id"));
        Assert.IsTrue(obj.HasProperty("Name"));
        Assert.IsTrue(obj.HasProperty("Posts"));
        Assert.IsTrue(obj.HasProperty("Slug"));
    }
    [TestMethod]
    public void Tag_HasData()
    {
        Assert.IsNotNull(testTag);
        Assert.AreEqual(1, testTag.Id);
        Assert.AreEqual("Test", testTag.Name);
        Assert.AreEqual("test", testTag.Slug);
        Assert.IsNotNull(testTag.Posts);
        Assert.AreEqual(0, testTag.Posts.Count);
    }
}
