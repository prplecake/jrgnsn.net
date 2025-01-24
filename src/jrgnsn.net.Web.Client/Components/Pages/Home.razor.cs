using jrgnsn.net.Web.Client.Models;
using Microsoft.AspNetCore.Components;

namespace jrgnsn.net.Web.Client.Components.Pages;

public partial class Home
{
    private readonly string blogPostsUrl = "api/v1/blog/posts";
    [Inject] private IHttpClientFactory _httpClientFactory { get; set; }
    public bool Loading { get; set; }
    protected List<Post>? Posts { get; set; } = new();
    private async Task LoadBlogPosts()
    {
        var httpClient = _httpClientFactory.CreateClient("ApiClient");
        var response = await httpClient.GetFromJsonAsync<List<Post>>(blogPostsUrl);
        Posts = response?.OrderByDescending(p => p.PublishDate).ToList();
    }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Loading = true;
            await LoadBlogPosts();
        }
        finally
        {
            Loading = false;
        }
    }
}
