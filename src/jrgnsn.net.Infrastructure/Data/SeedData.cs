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
        context.Posts.AddRange(
            new()
            {
                Id = 1,
                Title = "First Post",
                Author = "John Doe",
                PublishDate = DateTime.Parse("2021-01-01"),
                Content = "This is the first post on the blog. It's a test post to see how things work."
            },
            new()
            {
                Id = 2,
                Title = "Second Post",
                Author = "Jane Doe",
                PublishDate = DateTime.Parse("2021-02-01"),
                Content = "This is the second post on the blog. It's a test post to see how things work."
            }
        );
        context.SaveChanges();
    }
}
