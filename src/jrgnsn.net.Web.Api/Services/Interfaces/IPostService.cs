using jrgnsn.net.Web.Api.Services.Models;

namespace jrgnsn.net.Web.Api.Services.Interfaces;

public interface IPostService
{
    Task<PostDto?> GetPostByDateAndSlug(int year, int month, int day, string slug);
    Task<ICollection<PostDto>?> GetPosts();
}
