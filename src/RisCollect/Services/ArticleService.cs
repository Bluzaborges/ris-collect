using System.Text;
using System.Text.RegularExpressions;
using RisCollect.Data;
using RisCollect.Models;

namespace RisCollect.Services;

public partial class ArticleService
{
    private readonly ArticleRepository _articleRepository;

    public ArticleService(ArticleRepository repository)
    {
        _articleRepository = repository;
    }

    public List<Article> GetAll()
    {
        return _articleRepository.GetAll();
    }

    public (int Added, int Replaced, List<string> Errors) Import(
        IEnumerable<string> filePaths,
        string source,
        string searchQuery)
    {
        var added = 0;
        var replaced = 0;
        var errors = new List<string>();

        foreach (var filePath in filePaths)
        {
            try
            {
                var result = Save(Read(filePath, source, searchQuery));

                added += result.Added;
                replaced += result.Replaced;
            }
            catch (Exception exception)
            {
                errors.Add($"{Path.GetFileName(filePath)}: {exception.Message}");
            }
        }

        return (added, replaced, errors);
    }

    public void Delete(IEnumerable<Guid> articleIds)
    {
        _articleRepository.Delete(articleIds);
    }

    public void DeleteAll()
    {
        _articleRepository.DeleteAll();
    }

    private (int Added, int Replaced) Save(IEnumerable<Article> articles)
    {
        var candidates = articles.ToList();
        var existingArticles = _articleRepository
            .GetByIdentityKeys(candidates.Select(article => article.IdentityKey));

        var added = 0;
        var replaced = 0;

        foreach (var candidate in candidates)
        {
            if (existingArticles.TryGetValue(candidate.IdentityKey, out var existing))
            {
                existing.ReplaceWith(candidate);
                replaced++;
            }
            else
            {
                _articleRepository.Add(candidate);
                existingArticles.Add(candidate.IdentityKey, candidate);
                added++;
            }
        }

        _articleRepository.SaveChanges();

        return (added, replaced);
    }

    private static List<Article> Read(string filePath, string source, string searchQuery)
    {
        var articles = new List<Article>();
        var fields = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        string? lastTag = null;

        foreach (var rawLine in ReadText(filePath).Split(["\r\n", "\n", "\r"], StringSplitOptions.None))
        {
            var match = TagPattern().Match(rawLine);
            if (match.Success)
            {
                var tag = match.Groups["tag"].Value;
                var value = match.Groups["value"].Value.Trim();

                if (tag == "ER")
                {
                    AddArticle(articles, fields, source, searchQuery);
                    fields.Clear();
                    lastTag = null;
                    continue;
                }

                if (!fields.TryGetValue(tag, out var values))
                {
                    values = [];
                    fields[tag] = values;
                }

                values.Add(value);
                lastTag = tag;
            }
            else if (lastTag is not null && !string.IsNullOrWhiteSpace(rawLine))
            {
                var values = fields[lastTag];
                values[^1] = $"{values[^1]} {rawLine.Trim()}";
            }
        }

        AddArticle(articles, fields, source, searchQuery);

        return articles;
    }

    private static void AddArticle(
        ICollection<Article> articles,
        IReadOnlyDictionary<string, List<string>> fields,
        string source,
        string searchQuery)
    {
        if (fields.Count == 0)
            return;

        var title = First(fields, "TI", "T1");
        if (string.IsNullOrWhiteSpace(title))
            return;

        var doi = First(fields, "DO");
        var link = First(fields, "UR", "LK");

        if (string.IsNullOrWhiteSpace(link))
            link = doi;

        articles.Add(Article.Create(
            searchQuery: searchQuery,
            source: source,
            type: First(fields, "TY"),
            title: title,
            authors: Join(fields, ", ", "AU", "A1"),
            year: ParseYear(First(fields, "PY", "Y1", "DA")),
            journal: First(fields, "JO", "JF", "T2", "J1"),
            volume: First(fields, "VL"),
            issue: First(fields, "IS"),
            startPage: First(fields, "SP"),
            endPage: First(fields, "EP"),
            doi: doi,
            link: link,
            keywords: Join(fields, "; ", "KW"),
            abstractText: First(fields, "AB", "N2")));
    }

    private static string First(IReadOnlyDictionary<string, List<string>> fields, params string[] tags)
    {
        foreach (var tag in tags)
        {
            if (fields.TryGetValue(tag, out var values))
            {
                var value = values.FirstOrDefault(item => !string.IsNullOrWhiteSpace(item));

                if (value is not null)
                    return value;
            }
        }

        return string.Empty;
    }

    private static string Join(
        IReadOnlyDictionary<string, List<string>> fields,
        string separator,
        params string[] tags)
    {
        return string.Join(
            separator,
            tags.Where(fields.ContainsKey)
                .SelectMany(tag => fields[tag])
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Distinct(StringComparer.OrdinalIgnoreCase));
    }

    private static int? ParseYear(string value)
    {
        var match = YearPattern().Match(value);

        return match.Success && int.TryParse(match.Value, out var year) ? year : null;
    }

    private static string ReadText(string filePath)
    {
        var bytes = File.ReadAllBytes(filePath);

        try
        {
            return new UTF8Encoding(false, true).GetString(bytes);
        }
        catch (DecoderFallbackException)
        {
            return Encoding.Latin1.GetString(bytes);
        }
    }

    [GeneratedRegex(@"^(?<tag>[A-Z0-9]{2})\s{2}-\s?(?<value>.*)$")]
    private static partial Regex TagPattern();

    [GeneratedRegex(@"\b\d{4}\b")]
    private static partial Regex YearPattern();
}
