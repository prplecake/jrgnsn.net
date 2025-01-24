using jrgnsn.net.Core.Entities;
using jrgnsn.net.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jrgnsn.net.Web.Api.Controllers.Api.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BlogController : Controller
{
    private readonly BlogDbContext _context;
    public BlogController(BlogDbContext context)
    {
        _context = context;
    }
    [HttpGet("Posts")]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await _context.Posts.ToListAsync();
        return Ok(posts);
    }
    [HttpGet("Posts/{year:int}/{month:int}/{day:int}/{slug}")]
    public IActionResult GetPostByDateAndSlug(int year, int month, int day, string slug)
    {
        Console.WriteLine($"GetPostByDateAndSlug: {year}/{month}/{day}/{slug}");

        if (slug.EndsWith(".html"))
            slug = slug.Substring(0, slug.Length - 5);

        var post = _context.Posts.FirstOrDefault(p =>
            p.PublishDate.Year == year
            && p.PublishDate.Month == month
            && p.PublishDate.Day == day);
        if (post == null)
            return NotFound();
        if (post.Slug != slug)
            return NotFound();
        return Ok(post);
    }
}
