namespace jrgnsn.net.Web.Api.Services.Interfaces;

public interface IBlogTagService
{
    Task<BlogTagWithPostsDto?> GetTagBySlug(string slug);
    Task<ICollection<BlogTagDto>?> GetTags();
}
