using AutoMapper;
using jrgnsn.net.Core.Entities;
using jrgnsn.net.Web.Api.Services.Models;

namespace jrgnsn.net.Web.Api.Mapper;

public class SiteProfile : Profile
{
    public SiteProfile()
    {
        CreateMap<Post, PostDto>();
        CreateMap<Tag, BlogTagDto>();
        CreateMap<Tag, PostDto.TagDto>();
    }
}
