using jrgnsn.net.Infrastructure.Context;
using jrgnsn.net.Infrastructure.Data;
using jrgnsn.net.Web.Api.Extensions;
using jrgnsn.net.Web.Api.Mapper;
using jrgnsn.net.Web.Api.Services;
using jrgnsn.net.Web.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ILogger = Serilog.ILogger;

namespace jrgnsn.net.Web.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.ConfigureDatabase(builder.Configuration);

        builder.Services.AddAutoMapper(typeof(SiteProfile));
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<IBlogTagService, BlogTagService>();

        builder.Services.AddControllers();
        builder.Services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = false;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new QueryStringApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });
        builder.Services.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'V";
            opt.SubstituteApiVersionInUrl = true;
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

// Ensure to install the required NuGet package: Microsoft.AspNetCore.Mvc.Versioning

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        var logConfig = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console();
        if (app.Environment.IsDevelopment())
            logConfig.MinimumLevel.Verbose();
        Log.Logger = logConfig.CreateLogger();

        ILogger? logger = Log.ForContext<Program>();
        try
        {
            using var scope = app.Services.CreateScope();
            using var context = (BlogDbContext)scope.ServiceProvider.GetRequiredService(typeof(BlogDbContext));
            logger.Information("Ensuring database is created");
            context.Database.EnsureCreated();
            logger.Information("Attempting to apply any pending migrations");
            context.Database.Migrate();
            SeedData.Initialize(scope.ServiceProvider, builder.Configuration);
        }
        catch (Exception ex)
        {
            logger.Fatal(ex, "An error occurred while attempting to apply any pending migrations");
            Environment.Exit(1);
        }

        try
        {
            logger.Information("Starting the application");
            app.Run();
        }
        catch (Exception ex)
        {
            logger.Fatal(ex, "An error occurred while starting the application");
            Environment.Exit(1);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
