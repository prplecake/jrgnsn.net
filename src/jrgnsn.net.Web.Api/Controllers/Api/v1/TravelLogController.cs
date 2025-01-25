namespace jrgnsn.net.Web.Api.Controllers.Api.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TravelLogController : Controller
{
    private readonly ITravelLogService _travelLogService;
    public TravelLogController(ITravelLogService travelLogService)
    {
        _travelLogService = travelLogService;
    }
    [HttpGet("TravelLogs/{year:int}/{month:int}/{day:int}/{slug}")]
    public async Task<IActionResult> GetPostByDateAndSlug(int year, int month, int day, string slug)
    {
        Console.WriteLine($"GetPostByDateAndSlug: {year}/{month}/{day}/{slug}");

        if (slug.EndsWith(".html"))
            slug = slug.Substring(0, slug.Length - 5);

        var post = await _travelLogService.GetTravelLogByDateAndSlug(year, month, day, slug);
        if (post == null)
            return NotFound();
        return Ok(post);
    }
    [HttpGet("TravelLogs")]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await _travelLogService.GetTravelLogs();
        return Ok(posts);
    }
}
