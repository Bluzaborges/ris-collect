using Microsoft.EntityFrameworkCore;
using RisCollect.Models;

namespace RisCollect.Data;

public class ArticleRepository
{
    private readonly RisCollectDbContext _context;

    public ArticleRepository(RisCollectDbContext context)
    {
        _context = context;
    }

    public List<Article> GetAll()
    {
        return _context.Articles
            .AsNoTracking()
            .OrderBy(article => article.CreatedAt)
            .ToList();
    }

    public Dictionary<string, Article> GetByIdentityKeys(IEnumerable<string> identityKeys)
    {
        var keys = identityKeys
            .Distinct()
            .ToList();

        return _context.Articles
            .Where(article => keys.Contains(article.IdentityKey))
            .ToDictionary(article => article.IdentityKey, StringComparer.Ordinal);
    }

    public void Add(Article article)
    {
        _context.Articles.Add(article);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
    }

    public void DeleteAll()
    {
        _context.Articles.ExecuteDelete();
        _context.ChangeTracker.Clear();
    }

    public void Delete(IEnumerable<Guid> articleIds)
    {
        var ids = articleIds.ToList();

        if (ids.Count == 0)
            return;

        _context.Articles
            .Where(article => ids.Contains(article.ID))
            .ExecuteDelete();

        _context.ChangeTracker.Clear();
    }
}
