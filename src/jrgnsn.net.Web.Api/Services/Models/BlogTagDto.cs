using System.Text.Json.Serialization;

namespace jrgnsn.net.Web.Api.Services.Models;

public class BlogTagDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PostCount => Posts?.Count ?? 0;
    [JsonIgnore] public ICollection<PostDto>? Posts { get; set; }
    public string Slug { get; set; }
}
