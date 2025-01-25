namespace jrgnsn.net.Web.Client.Models;

public class Tag
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int PostCount { get; set; }
    public required string Slug { get; set; }
}
