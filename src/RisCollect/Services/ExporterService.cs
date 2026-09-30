using System.Text;
using RisCollect.Models;

namespace RisCollect.Services;

public class ExporterService
{
    private static readonly string[] Headers =
    [
        "Search Query",
        "Source",
        "Type",
        "Title",
        "Authors",
        "Year",
        "Journal",
        "Volume",
        "Issue",
        "Start Page",
        "End Page",
        "DOI",
        "Link",
        "Keywords",
        "Abstract"
    ];

    public void Export(string filePath, IEnumerable<Article> articles)
    {
        using var writer = new StreamWriter(filePath, false, new UTF8Encoding(true));

        writer.WriteLine(string.Join(',', Headers.Select(Escape)));

        foreach (var article in articles)
        {
            string[] values =
            [
                article.SearchQuery,
                article.Source,
                article.Type,
                article.Title,
                article.Authors,
                article.Year?.ToString() ?? string.Empty,
                article.Journal,
                article.Volume,
                article.Issue,
                article.StartPage,
                article.EndPage,
                article.Doi,
                article.Link,
                article.Keywords,
                article.Abstract
            ];

            writer.WriteLine(string.Join(',', values.Select(Escape)));
        }
    }

    private static string Escape(string value)
    {
        return $"\"{value.Replace("\"", "\"\"")}\"";
    }
}
