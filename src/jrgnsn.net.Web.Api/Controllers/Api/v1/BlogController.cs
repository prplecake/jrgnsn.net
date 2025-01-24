using jrgnsn.net.Core.Entities;
using jrgnsn.net.Infrastructure.Context;
using jrgnsn.net.Web.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jrgnsn.net.Web.Api.Controllers.Api.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BlogController : Controller
{
    private readonly IPostService _postService;
    private readonly IBlogTagService _blogTagService;
    public BlogController(IPostService postService, IBlogTagService blogTagService)
    {
        _postService = postService;
        _blogTagService = blogTagService;
    }
    [HttpGet("Posts")]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await _postService.GetPosts();
        return Ok(posts);
    }
    [HttpGet("Posts/{year:int}/{month:int}/{day:int}/{slug}")]
    public async Task<IActionResult> GetPostByDateAndSlug(int year, int month, int day, string slug)
    {
        Console.WriteLine($"GetPostByDateAndSlug: {year}/{month}/{day}/{slug}");

        if (slug.EndsWith(".html"))
            slug = slug.Substring(0, slug.Length - 5);

        var post = await _postService.GetPostByDateAndSlug(year, month, day, slug);
        if (post == null)
            return NotFound();
        return Ok(post);
    }
    [HttpGet("Tags")]
    public async Task<IActionResult> GetTags()
    {
        var tags = await _blogTagService.GetTags();
        return Ok(tags);
    }
    [HttpGet("Tags/{slug}")]
    public async Task<IActionResult> GetTagBySlug(string slug)
    {
        var tag = await _blogTagService.GetTagBySlug(slug);
        if (tag == null)
            return NotFound();
        return Ok(tag);
    }
}
