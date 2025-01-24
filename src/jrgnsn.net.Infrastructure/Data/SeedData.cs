using jrgnsn.net.Core.Entities;
using jrgnsn.net.Core.Extensions;
using jrgnsn.net.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace jrgnsn.net.Infrastructure.Data;

public class SeedData
{
    private static readonly ILogger Logger = Log.ForContext<SeedData>();
    public static void Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using var context = new BlogDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<BlogDbContext>>(), configuration);

        if (context is null || context.Posts is null)
            throw new NullReferenceException("BlogDbContext or BlogPosts DbSet is null");
        if (context.Posts.Any())
            return; // Data already seeded
        Logger.Information("Seeding data");
        Tag testTag1 = new() { Name = "Test Tag 1", Slug = "test-tag-1" };
        Tag testTag2 = new() { Name = "Test Tag 2", Slug = "test-tag-2" };
        Tag testTag3 = new() { Name = "Test Tag 3", Slug = "test-tag-3" };
        context.Posts.AddRange(
            new Post
            {
                Id = 1,
                Title = "First Post",
                Slug = "First Post".ToUrlSlug(),
                PublishDate = DateTime.Parse("2021-01-01"),
                Content = "This is the first post on the blog. It's a test post to see how things work.",
                Tags = [testTag1, testTag2]
            },
            new Post
            {
                Id = 2,
                Title = "Second Post",
                Slug = "Second Post".ToUrlSlug(),
                PublishDate = DateTime.Parse("2021-02-01"),
                Content = "This is the second post on the blog. It's a test post to see how things work.",
                Tags = [testTag2, testTag3]
            }
        );
        context.SaveChanges();
    }
}
