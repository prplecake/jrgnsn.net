using Markdig;
using Microsoft.AspNetCore.Components;

namespace jrgnsn.net.Web.Client;

public class MarkdownParser
{
    private readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
        .Build();
    public MarkupString? ToHtml(string? markdown) => markdown is null ? null : new MarkupString(Markdown.ToHtml(markdown, _pipeline));
}
