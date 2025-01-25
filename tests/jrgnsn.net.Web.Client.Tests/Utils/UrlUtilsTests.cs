using jrgnsn.net.Web.Client.Tests.Data;
using jrgnsn.net.Web.Client.Utils;

namespace jrgnsn.net.Web.Client.Tests.Utils;

[TestClass]
public class UrlUtilsTests
{
    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void GetPermalink_Throws_NotImplementedException()
    {
        // Act
        UrlUtils.GetPermalink(new object());
    }
    [TestMethod]
    public void Post_GetPermalink()
    {
        // Act
        string result = UrlUtils.GetPermalink(TestData.TestPost);

        // Assert
        Assert.AreEqual("/2021/01/01/test-post", result);
    }
    [TestMethod]
    public void TravelLog_GetPermalink()
    {
        // Act
        string result = UrlUtils.GetPermalink(Core.Tests.Data.TestData.TestTravelLog);

        // Assert
        Assert.AreEqual("/travel/2017-11-01-chicago-il", result);
    }
}
