namespace jrgnsn.net.Web.Api.Services;

public class PostService : IPostService
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;
    public PostService(BlogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PostDto?> GetPostByDateAndSlug(int year, int month, int day, string slug)
    {
        if (slug.EndsWith(".html"))
            slug = slug.Substring(0, slug.Length - 5);

        var post = await _context.Posts
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p =>
                p.PublishDate.Year == year
                && p.PublishDate.Month == month
                && p.PublishDate.Day == day);
        if (post == null)
            return null;
        if (post.Slug != slug)
            return null;
        return _mapper.Map<PostDto>(post);
    }
    public async Task<ICollection<PostDto>> GetPosts()
    {
        var posts = await _context.Posts
            .Include(p => p.Tags)
            .ToListAsync();
        return _mapper.Map<List<PostDto>>(posts);
    }
}
