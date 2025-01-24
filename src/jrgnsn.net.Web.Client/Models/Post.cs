namespace jrgnsn.net.Web.Client.Models;

public class Post
{
    public string Content { get; set; }
    public int Id { get; set; }
    public DateTime PublishDate { get; set; }
    public string Slug { get; set; }
    public string Summary { get; set; }
    public List<Tag> Tags { get; set; }
    public string Title { get; set; }
}
