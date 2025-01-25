namespace jrgnsn.net.Web.Api.Services.Interfaces;

public interface IPostService
{
    Task<PostWithTagsDto?> GetPostByDateAndSlug(int year, int month, int day, string slug);
    Task<ICollection<PostWithTagsDto>?> GetPosts();
}
