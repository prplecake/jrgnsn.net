using jrgnsn.net.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace jrgnsn.net.Infrastructure.Context;

public class BlogDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public BlogDbContext(DbContextOptions<BlogDbContext> options, IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public DbSet<BlogPost> BlogPosts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // using SQLite
        string connectionString = _configuration.GetConnectionString("Default") ??
                                  throw new InvalidOperationException("Connection string 'Default' not found");
        optionsBuilder.UseSqlite(connectionString);
    }
}
