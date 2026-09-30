using System.ComponentModel;
using RisCollect.Models;
using RisCollect.Services;

namespace RisCollect.Forms;

public partial class MainForm : Form
{
    private readonly BindingList<Article> _articles = [];
    private readonly ArticleService _articleService;
    private readonly ExporterService _exporterService;

    public MainForm(
        ArticleService articleService,
        ExporterService exporterService)
    {
        _articleService = articleService;
        _exporterService = exporterService;

        InitializeComponent();
        SetApplicationIcon();

        articleGrid.DataSource = _articles;
        
        LoadArticles();
    }

    private void SetApplicationIcon()
    {
        var applicationIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

        if (applicationIcon is not null)
            Icon = applicationIcon;
    }

    private void ImportFiles(object? sender, EventArgs eventArgs)
    {
        var source = sourceTextBox.Text.Trim();
        if (source.Length == 0)
        {
            MessageBox.Show(
                this,
                "Enter the database or platform from which the files were exported.",
                "Source required",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            sourceTextBox.Focus();
            return;
        }

        var searchQuery = searchQueryBox.Text.Trim();
        if (searchQuery.Length == 0)
        {
            MessageBox.Show(
                this,
                "Enter the search query used to obtain these results.",
                "Search query required",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            searchQueryBox.Focus();
            return;
        }

        using var dialog = new OpenFileDialog
        {
            Title = "Select RIS files",
            Filter = "RIS files (*.ris)|*.ris|All files (*.*)|*.*",
            Multiselect = true,
            CheckFileExists = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        (int Added, int Replaced, List<string> Errors) result;

        UseWaitCursor = true;
        try
        {
            result = _articleService.Import(dialog.FileNames, source, searchQuery);
            LoadArticles($"Import complete: {result.Added} added, {result.Replaced} replaced.");
        }
        finally
        {
            UseWaitCursor = false;
        }

        if (result.Errors.Count > 0)
        {
            MessageBox.Show(
                this,
                string.Join(Environment.NewLine, result.Errors),
                "Some files could not be imported",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void ExportCsv(object? sender, EventArgs eventArgs)
    {
        if (_articles.Count == 0)
            return;

        using var dialog = new SaveFileDialog
        {
            Title = "Export articles to CSV",
            Filter = "CSV files (*.csv)|*.csv",
            DefaultExt = "csv",
            AddExtension = true,
            FileName = $"riscollect-{DateTime.Now:yyyyMMdd}.csv"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            _exporterService.Export(dialog.FileName, _articles);

            UpdateStatus($"Exported {_articles.Count} articles to {Path.GetFileName(dialog.FileName)}.");
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                this,
                exception.Message,
                "CSV export failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void DeleteArticles(object? sender, EventArgs eventArgs)
    {
        if (_articles.Count == 0)
            return;

        var selectedArticles = articleGrid.SelectedRows
            .Cast<DataGridViewRow>()
            .Select(row => row.DataBoundItem)
            .OfType<Article>()
            .ToList();

        var deleteSelected = selectedArticles.Count > 0;
        var selectedMessage = selectedArticles.Count == 1
            ? "Permanently remove the selected article?"
            : $"Permanently remove the {selectedArticles.Count} selected articles?";

        var answer = MessageBox.Show(
            this,
            deleteSelected
                ? selectedMessage
                : "Permanently remove every imported article?",
            deleteSelected ? "Delete selected" : "Clear collection",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (answer != DialogResult.Yes)
            return;

        if (deleteSelected)
            _articleService.Delete(selectedArticles.Select(article => article.ID));
        else
            _articleService.DeleteAll();

        LoadArticles(deleteSelected
            ? selectedArticles.Count == 1
                ? "1 article deleted."
                : $"{selectedArticles.Count} articles deleted."
            : "The collection was cleared.");
    }

    private void LoadArticles(string? message = null)
    {
        _articles.RaiseListChangedEvents = false;
        _articles.Clear();

        foreach (var article in _articleService.GetAll())
            _articles.Add(article);

        _articles.RaiseListChangedEvents = true;
        _articles.ResetBindings();
        articleGrid.ClearSelection();
        articleGrid.CurrentCell = null;

        UpdateStatus(message);
    }

    private void UpdateStatus(string? message = null)
    {
        var hasArticles = _articles.Count > 0;

        statusLabel.Text = message ?? $"{_articles.Count} articles saved locally";

        exportButton.Enabled = hasArticles;
        exportButton.BackColor = hasArticles
            ? Color.FromArgb(0, 72, 232)
            : Color.FromArgb(232, 232, 232);

        clearButton.Enabled = hasArticles;
        clearButton.BackColor = hasArticles
            ? Color.FromArgb(243, 243, 243)
            : Color.FromArgb(232, 232, 232);

        UpdateDeleteButton(null, EventArgs.Empty);
    }

    private void UpdateDeleteButton(object? sender, EventArgs eventArgs)
    {
        clearButton.Text = articleGrid.SelectedRows.Count > 0
            ? "Delete selected"
            : "Clear collection";
    }

    private void ClearArticleSelection(object? sender, MouseEventArgs eventArgs)
    {
        articleGrid.ClearSelection();
        articleGrid.CurrentCell = null;
    }

    private void ClearArticleSelectionOutsideRows(object? sender, MouseEventArgs eventArgs)
    {
        if (articleGrid.HitTest(eventArgs.X, eventArgs.Y).RowIndex < 0)
            ClearArticleSelection(sender, eventArgs);
    }

    private void PaintCellWithoutFocusRectangle(object? sender, DataGridViewCellPaintingEventArgs eventArgs)
    {
        if (eventArgs.RowIndex < 0)
            return;

        eventArgs.Paint(
            eventArgs.CellBounds,
            eventArgs.PaintParts & ~DataGridViewPaintParts.Focus);

        eventArgs.Handled = true;
    }
}
