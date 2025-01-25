using jrgnsn.net.Core.Entities;
using jrgnsn.net.Web.Client.Models;
using Microsoft.AspNetCore.Components;

namespace jrgnsn.net.Web.Client.Components.Pages;

public partial class AllTravelLog
{
    private readonly string travelLogUrl = "api/v1/travellog/trips";
    [Inject] private IHttpClientFactory _httpClientFactory { get; set; }
    public bool Loading { get; set; }
    protected List<TravelLog>? Trips { get; set; } = new();
    private async Task LoadTrips()
    {
        var httpClient = _httpClientFactory.CreateClient("ApiClient");
        var response = await httpClient.GetFromJsonAsync<List<TravelLog>>(travelLogUrl);
        Trips = response?.OrderByDescending(tl => tl.StartDate).ToList();
    }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Loading = true;
            await LoadTrips();
        }
        finally
        {
            Loading = false;
        }
    }
}
