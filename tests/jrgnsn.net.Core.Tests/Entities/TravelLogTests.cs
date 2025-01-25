using jrgnsn.net.Core.Tests.Data;

namespace jrgnsn.net.Core.Tests.Entities;

[TestClass]
public class TravelLogTests
{
    [TestMethod]
    public void TravelLog_HasProperties()
    {
        var obj = TestData.TestTravelLog;

        // Assert
        Assert.AreEqual(6, obj.PropertyCount());
        Assert.IsTrue(obj.HasProperty("Content"));
        Assert.IsTrue(obj.HasProperty("Destination"));
        Assert.IsTrue(obj.HasProperty("EndDate"));
        Assert.IsTrue(obj.HasProperty("Id"));
        Assert.IsTrue(obj.HasProperty("StartDate"));
        Assert.IsTrue(obj.HasProperty("Slug"));
    }
    [TestMethod]
    public void TestTravelLog_HasData()
    {
        var travelLog = TestData.TestTravelLog;
        Assert.IsNotNull(travelLog);
        Assert.AreEqual(1, travelLog.Id);
        Assert.AreEqual("Chicago, IL", travelLog.Destination);
        Assert.AreEqual(new DateOnly(2017, 11, 30), travelLog.EndDate);
        Assert.AreEqual(new DateOnly(2017, 11, 1), travelLog.StartDate);
        Assert.AreEqual("chicago-il", travelLog.Slug);
        Assert.IsNotNull(travelLog.Content);
    }
}
