using jrgnsn.net.Web.Client.Utils;

namespace jrgnsn.net.Web.Client.Tests.Utils;

[TestClass]
public class DateUtilsTests
{
    [TestMethod]
    public void DateOnly_ToDateString_Tests()
    {
        // Arrange
        var date = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var dateString = date.ToDateString();

        // Assert
        Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd"), dateString);
    }
    [TestMethod]
    public void DateTime_ToDateString_Tests()
    {
        // Arrange
        var date = DateTime.Now;

        // Act
        var dateString = date.ToDateString();

        // Assert
        Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd"), dateString);
    }
}
