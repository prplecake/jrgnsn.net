using System.Text;
using System.Text.RegularExpressions;

namespace jrgnsn.net.Core.Extensions;

public static class StringExtensions
{
    public static string ToUrlSlug(this string input)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var value = input.ToLowerInvariant().Trim();

        // Remove accents
        byte[] bytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(value);
        value = Encoding.ASCII.GetString(bytes);

        value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);
        value = Regex.Replace(value, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);
        value = value.Trim('-', '_');
        value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);
        return value;
    }
}
