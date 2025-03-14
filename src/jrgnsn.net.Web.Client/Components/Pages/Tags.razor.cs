using jrgnsn.net.Web.Client.Models;
using Microsoft.AspNetCore.Components;

namespace jrgnsn.net.Web.Client.Components.Pages;

public partial class Tags
{
    private readonly string blogTagsUrl = "api/v1/blog/tags";
    [Inject] private IHttpClientFactory? HttpClientFactory { get; set; }
    public bool Loading { get; set; }
    protected List<Tag>? TagList { get; set; }
    private async Task LoadBlogTags()
    {
        var httpClient = HttpClientFactory?.CreateClient("ApiClient") ?? throw new Exception("Could not create HttpClient");
        var response = await httpClient.GetFromJsonAsync<List<Tag>>(blogTagsUrl);
        TagList = response?.ToList();
    }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Loading = true;
            await LoadBlogTags();
        }
        finally
        {
            Loading = false;
        }
    }
}
