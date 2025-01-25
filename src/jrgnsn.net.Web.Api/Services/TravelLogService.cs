namespace jrgnsn.net.Web.Api.Services;

public class TravelLogService : ITravelLogService
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;
    public TravelLogService(BlogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<TravelLog?> GetTravelLogByDateAndSlug(int year, int month, int day, string slug)
    {
        if (slug.EndsWith(".html"))
            slug = slug.Substring(0, slug.Length - 5);

        var trip = await _context.TravelLogs
            .FirstOrDefaultAsync(tl =>
                tl.StartDate.Year == year
                && tl.StartDate.Month == month
                && tl.StartDate.Day == day);
        if (trip == null)
            return null;
        if (trip.Slug != slug)
            return null;
        return _mapper.Map<TravelLog>(trip);
    }
    public async Task<ICollection<TravelLog>?> GetTravelLogs()
    {
        var trips = await _context.TravelLogs
            .ToListAsync();
        return _mapper.Map<List<TravelLog>>(trips);
    }
}
