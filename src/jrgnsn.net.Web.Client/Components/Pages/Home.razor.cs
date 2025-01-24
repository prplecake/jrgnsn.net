using jrgnsn.net.Core.Entities;
using Microsoft.AspNetCore.Components;

namespace jrgnsn.net.Web.Client.Components.Pages;

public partial class Home
{
    private readonly string blogPostsUrl = "api/v1/blog/posts";
    [Inject] private IHttpClientFactory _httpClientFactory { get; set; }
    protected List<Post> Posts { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await LoadBlogPosts();
    }
    private async Task LoadBlogPosts()
    {
        var httpClient = _httpClientFactory.CreateClient("ApiClient");
        var response = await httpClient.GetFromJsonAsync<List<Post>>(blogPostsUrl);
        Posts = response.OrderByDescending(p => p.PublishDate).ToList();
    }
}
