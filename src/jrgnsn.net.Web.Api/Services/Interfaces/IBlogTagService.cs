namespace jrgnsn.net.Web.Api.Services.Interfaces;

public interface IBlogTagService
{
    Task<BlogTagDto?> GetTagBySlug(string slug);
    Task<ICollection<BlogTagDto>?> GetTags();
}
