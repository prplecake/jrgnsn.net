namespace jrgnsn.net.Web.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // using SQLite
        string connectionString = configuration.GetConnectionString("Default") ??
                                  throw new InvalidOperationException("Connection string 'Default' not found");
        services.AddDbContext<BlogDbContext>(options => options.UseSqlite(connectionString));
    }
}
