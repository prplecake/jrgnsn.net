namespace jrgnsn.net.Core.Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublishDate { get; set; }
    public string Content { get; set; }
    public string Summary
    {
        get
        {
            if (Content.Length > 50)
                return Content.Substring(0, 50) + "...";
            return Content;
        }
    }
    public string GetPermalink()
    {
        var year = PublishDate.Year;
        var month = PublishDate.Month.ToString().PadLeft(2, '0');
        var day = PublishDate.Day.ToString().PadLeft(2, '0');
        return $"/{year}/{month}/{day}/{Slug}";
    }
    public string Slug => Title.Replace(" ", "-");
}
