using jrgnsn.net.Core.Extensions;

namespace jrgnsn.net.Core.Entities;

public class Tag
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public List<Post>? Posts { get; set; }
}
