using Microsoft.EntityFrameworkCore;
using RisCollect.Models;

namespace RisCollect.Data;

public class RisCollectDbContext : DbContext
{
    public DbSet<Article> Articles => Set<Article>();

    public RisCollectDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dataDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RisCollect");

        Directory.CreateDirectory(dataDirectory);
        optionsBuilder.UseSqlite(
            $"Data Source={Path.Combine(dataDirectory, "riscollect.sqlite")};Pooling=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var article = modelBuilder.Entity<Article>();

        article.ToTable("articles");
        article.HasKey(item => item.ID);
        article.HasIndex(item => item.IdentityKey)
            .IsUnique();

        article.Property(item => item.ID)
            .HasColumnName("id")
            .ValueGeneratedNever();

        article.Property(item => item.IdentityKey)
            .HasColumnName("identity_key")
            .IsRequired();

        article.Property(item => item.SearchQuery)
            .HasColumnName("search_query")
            .IsRequired();

        article.Property(item => item.Source)
            .HasColumnName("source")
            .IsRequired();

        article.Property(item => item.Type)
            .HasColumnName("type")
            .IsRequired();

        article.Property(item => item.Title)
            .HasColumnName("title")
            .IsRequired();

        article.Property(item => item.Authors)
            .HasColumnName("authors")
            .IsRequired();

        article.Property(item => item.Year)
            .HasColumnName("year");

        article.Property(item => item.Journal)
            .HasColumnName("journal")
            .IsRequired();

        article.Property(item => item.Volume)
            .HasColumnName("volume")
            .IsRequired();

        article.Property(item => item.Issue)
            .HasColumnName("issue")
            .IsRequired();

        article.Property(item => item.StartPage)
            .HasColumnName("start_page")
            .IsRequired();

        article.Property(item => item.EndPage)
            .HasColumnName("end_page")
            .IsRequired();

        article.Property(item => item.Doi)
            .HasColumnName("doi")
            .IsRequired();

        article.Property(item => item.Link)
            .HasColumnName("link")
            .IsRequired();

        article.Property(item => item.Keywords)
            .HasColumnName("keywords")
            .IsRequired();

        article.Property(item => item.Abstract)
            .HasColumnName("abstract")
            .IsRequired();

        article.Property(item => item.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        article.Property(item => item.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();
    }
}
