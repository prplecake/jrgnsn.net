using jrgnsn.net.Core.Extensions;

namespace jrgnsn.net.Web.Api.Services.Models;

public class PostDto
{
    public string Author { get; set; }
    public string Content { get; set; }
    public int Id { get; set; }
    public DateTime PublishDate { get; set; }
    public string Slug { get; set; }
    public string Summary
    {
        get
        {
            if (Content.Length > 50)
                return Content.Substring(0, 50) + "...";
            return Content;
        }
    }
    public List<TagDto>? Tags { get; set; }
    public string Title { get; set; }
    public string GetPermalink()
    {
        int year = PublishDate.Year;
        string month = PublishDate.Month.ToString().PadLeft(2, '0');
        string day = PublishDate.Day.ToString().PadLeft(2, '0');
        return $"/{year}/{month}/{day}/{Slug}";
    }
    public class TagDto
    {
        public string Name { get; set; }
        public string Slug => Name.ToUrlSlug();
    }
}
