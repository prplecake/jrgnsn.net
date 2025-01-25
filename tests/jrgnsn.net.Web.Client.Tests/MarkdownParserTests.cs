namespace jrgnsn.net.Web.Client.Tests;

[TestClass]
public class MarkdownParserTests
{
    private MarkdownParser _parser = null!;
    [TestInitialize]
    public void Init()
    {
        _parser = new MarkdownParser();
    }
    [DataTestMethod]
    [DataRow("Hello, world!", "<p>Hello, world!</p>")]
    [DataRow("Hello, **world**!", "<p>Hello, <strong>world</strong>!</p>")]
    [DataRow("Hello, [world](https://example.com)!", "<p>Hello, <a href=\"https://example.com\">world</a>!</p>")]
    [DataRow("Hello, `world`!", "<p>Hello, <code>world</code>!</p>")]
    public void ToHtml_Simple(string input, string expected)
    {
        // Act
        var htmlString = _parser.ToHtml(input).Value.ToString().Trim();

        // Assert
        Assert.AreEqual(expected, htmlString);
    }
}
