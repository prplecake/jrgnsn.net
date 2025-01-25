namespace jrgnsn.net.Web.Api.Mapper;

public class SiteProfile : Profile
{
    public SiteProfile()
    {
        CreateMap<Post, PostDto>();
        CreateMap<Post, PostWithTagsDto>();
        CreateMap<Tag, BlogTagDto>();
        CreateMap<Tag, PostWithTagsDto.TagDto>();
        CreateMap<Tag, BlogTagWithPostsDto>();
    }
}
