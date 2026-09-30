using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace RisCollect.Models;

public class Article
{
    public Guid ID { get; private set; }
    public string IdentityKey { get; private set; }
    public string SearchQuery { get; private set; }
    public string Source { get; private set; }
    public string Type { get; private set; }
    public string Title { get; private set; }
    public string Authors { get; private set; }
    public int? Year { get; private set; }
    public string Journal { get; private set; }
    public string Volume { get; private set; }
    public string Issue { get; private set; }
    public string StartPage { get; private set; }
    public string EndPage { get; private set; }
    public string Doi { get; private set; }
    public string Link { get; private set; }
    public string Keywords { get; private set; }
    public string Abstract { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Article()
    {
        IdentityKey = string.Empty;
        SearchQuery = string.Empty;
        Source = string.Empty;
        Type = string.Empty;
        Title = string.Empty;
        Authors = string.Empty;
        Journal = string.Empty;
        Volume = string.Empty;
        Issue = string.Empty;
        StartPage = string.Empty;
        EndPage = string.Empty;
        Doi = string.Empty;
        Link = string.Empty;
        Keywords = string.Empty;
        Abstract = string.Empty;
    }

    public static Article Create(
        string searchQuery,
        string source,
        string type,
        string title,
        string authors,
        int? year,
        string journal,
        string volume,
        string issue,
        string startPage,
        string endPage,
        string doi,
        string link,
        string keywords,
        string abstractText)
    {
        var article = new Article
        {
            ID = Guid.NewGuid(),
            SearchQuery = searchQuery.Trim(),
            Source = source.Trim(),
            Type = type.Trim(),
            Title = title.Trim(),
            Authors = authors.Trim(),
            Year = year,
            Journal = journal.Trim(),
            Volume = volume.Trim(),
            Issue = issue.Trim(),
            StartPage = startPage.Trim(),
            EndPage = endPage.Trim(),
            Doi = doi.Trim(),
            Link = link.Trim(),
            Keywords = keywords.Trim(),
            Abstract = abstractText.Trim(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        article.IdentityKey = CreateIdentityKey(article);
        return article;
    }

    internal void ReplaceWith(Article replacement)
    {
        SearchQuery = replacement.SearchQuery;
        Source = replacement.Source;
        Type = replacement.Type;
        Title = replacement.Title;
        Authors = replacement.Authors;
        Year = replacement.Year;
        Journal = replacement.Journal;
        Volume = replacement.Volume;
        Issue = replacement.Issue;
        StartPage = replacement.StartPage;
        EndPage = replacement.EndPage;
        Doi = replacement.Doi;
        Link = replacement.Link;
        Keywords = replacement.Keywords;
        Abstract = replacement.Abstract;
        UpdatedAt = DateTime.UtcNow;
    }

    private static string CreateIdentityKey(Article article)
    {
        var identity = string.Join(
            '\u001f',
            Normalize(article.Title),
            Normalize(article.SearchQuery),
            Normalize(article.Source),
            Normalize(article.Journal));

        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(identity)));
    }

    private static string Normalize(string value)
    {
        return Regex.Replace(value.Trim(), @"\s+", " ").ToUpperInvariant();
    }
}
