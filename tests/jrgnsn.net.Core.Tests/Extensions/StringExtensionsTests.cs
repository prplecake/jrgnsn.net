using jrgnsn.net.Core.Extensions;

namespace jrgnsn.net.Core.Tests.Extensions;

[TestClass]
public class StringExtensionsTests
{
    [DataTestMethod]
    [DataRow("St. Paul", "st-paul")]
    [DataRow("Hopkins", "hopkins")]
    [DataRow("O'Fallon", "ofallon")]
    [DataRow("Dover-Foxcroft", "dover-foxcroft")]
    public void ToUrlSlug(string input, string expected)
    {
        Assert.AreEqual(expected, input.ToUrlSlug());
    }
}
