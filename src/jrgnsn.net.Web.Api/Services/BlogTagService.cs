namespace jrgnsn.net.Web.Api.Services;

public class BlogTagService : IBlogTagService
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;
    public BlogTagService(BlogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<BlogTagWithPostsDto?> GetTagBySlug(string slug)
    {
        var tag = await _context.Tags
            .Include(t => t.Posts)
            .FirstOrDefaultAsync(t => t.Slug == slug);
        return tag is null ? null : _mapper.Map<BlogTagWithPostsDto>(tag);
    }
    public async Task<ICollection<BlogTagDto>?> GetTags()
    {
        var tags = await _context.Tags
            .Include(t => t.Posts).IgnoreAutoIncludes()
            .ToListAsync();
        return _mapper.Map<ICollection<BlogTagDto>>(tags);
    }
}
