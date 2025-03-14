namespace jrgnsn.net.Web.Api.Services.Models;

public class BlogTagWithPostsDto : BlogTagDto
{
    public int PostCount => Posts?.Count ?? 0;
    public ICollection<PostDto>? Posts { get; set; }
}
