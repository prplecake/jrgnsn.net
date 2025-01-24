namespace jrgnsn.net.Core.Entities;

public class Post
{
    public required string Content { get; set; }
    public int Id { get; set; }
    public DateTime PublishDate { get; set; }
    public required string Slug { get; set; }
    public List<Tag>? Tags { get; set; }
    public required string Title { get; set; }
}
