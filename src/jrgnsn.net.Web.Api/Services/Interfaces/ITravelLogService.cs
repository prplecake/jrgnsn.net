namespace jrgnsn.net.Web.Api.Services.Interfaces;

public interface ITravelLogService
{
    Task<TravelLog?> GetTravelLogByDateAndSlug(int year, int month, int day, string slug);
    Task<ICollection<TravelLog>?> GetTravelLogs();
}
