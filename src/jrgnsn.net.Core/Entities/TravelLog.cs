namespace jrgnsn.net.Core.Entities;

public class TravelLog
{
    public string? Content { get; set; }
    public required string Destination { get; set; }
    public DateOnly EndDate { get; set; }
    public int Id { get; set; }
    public required string Slug { get; set; }
    public DateOnly StartDate { get; set; }
}
