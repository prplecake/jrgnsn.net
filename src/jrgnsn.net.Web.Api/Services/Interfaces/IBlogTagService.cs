using jrgnsn.net.Web.Api.Services.Models;

namespace jrgnsn.net.Web.Api.Services.Interfaces;

public interface IBlogTagService
{
    Task<ICollection<BlogTagDto>?> GetTags();
    Task<BlogTagDto?> GetTagBySlug(string slug);
}
