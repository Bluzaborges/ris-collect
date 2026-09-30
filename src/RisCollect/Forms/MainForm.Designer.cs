namespace RisCollect.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel inputPanel;
    private Label sourceLabel;
    private TextBox sourceTextBox;
    private Label searchQueryLabel;
    private TextBox searchQueryBox;
    private Button importButton;
    private Panel contentPanel;
    private DataGridView articleGrid;
    private DataGridViewTextBoxColumn searchQueryColumn;
    private DataGridViewTextBoxColumn sourceColumn;
    private DataGridViewTextBoxColumn titleColumn;
    private DataGridViewTextBoxColumn authorsColumn;
    private DataGridViewTextBoxColumn yearColumn;
    private DataGridViewTextBoxColumn journalColumn;
    private Panel footerPanel;
    private Label statusLabel;
    private Button exportButton;
    private Button clearButton;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        DataGridViewCellStyle alternatingRowsStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle columnHeadersStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle cellsStyle = new DataGridViewCellStyle();
        components = new System.ComponentModel.Container();
        inputPanel = new Panel();
        sourceLabel = new Label();
        sourceTextBox = new TextBox();
        searchQueryLabel = new Label();
        searchQueryBox = new TextBox();
        importButton = new Button();
        contentPanel = new Panel();
        articleGrid = new DataGridView();
        searchQueryColumn = new DataGridViewTextBoxColumn();
        sourceColumn = new DataGridViewTextBoxColumn();
        titleColumn = new DataGridViewTextBoxColumn();
        authorsColumn = new DataGridViewTextBoxColumn();
        yearColumn = new DataGridViewTextBoxColumn();
        journalColumn = new DataGridViewTextBoxColumn();
        footerPanel = new Panel();
        statusLabel = new Label();
        exportButton = new Button();
        clearButton = new Button();
        inputPanel.SuspendLayout();
        contentPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)articleGrid).BeginInit();
        footerPanel.SuspendLayout();
        SuspendLayout();

        inputPanel.BackColor = Color.White;
        inputPanel.Controls.Add(sourceLabel);
        inputPanel.Controls.Add(sourceTextBox);
        inputPanel.Controls.Add(searchQueryLabel);
        inputPanel.Controls.Add(searchQueryBox);
        inputPanel.Controls.Add(importButton);
        inputPanel.Dock = DockStyle.Top;
        inputPanel.Location = new Point(0, 0);
        inputPanel.Name = "inputPanel";
        inputPanel.Size = new Size(1084, 88);
        inputPanel.TabIndex = 0;
        inputPanel.MouseDown += ClearArticleSelection;

        sourceLabel.AutoSize = true;
        sourceLabel.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
        sourceLabel.ForeColor = Color.FromArgb(75, 75, 75);
        sourceLabel.Location = new Point(20, 14);
        sourceLabel.Name = "sourceLabel";
        sourceLabel.Text = "SOURCE";
        sourceLabel.MouseDown += ClearArticleSelection;

        sourceTextBox.BackColor = Color.White;
        sourceTextBox.BorderStyle = BorderStyle.FixedSingle;
        sourceTextBox.Font = new Font("Segoe UI", 10F);
        sourceTextBox.ForeColor = Color.FromArgb(32, 32, 32);
        sourceTextBox.Location = new Point(20, 36);
        sourceTextBox.Name = "sourceTextBox";
        sourceTextBox.PlaceholderText = "Database or platform name";
        sourceTextBox.Size = new Size(210, 25);
        sourceTextBox.TabIndex = 0;
        sourceTextBox.MouseDown += ClearArticleSelection;

        searchQueryLabel.AutoSize = true;
        searchQueryLabel.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
        searchQueryLabel.ForeColor = Color.FromArgb(75, 75, 75);
        searchQueryLabel.Location = new Point(250, 14);
        searchQueryLabel.Name = "searchQueryLabel";
        searchQueryLabel.Text = "SEARCH QUERY";
        searchQueryLabel.MouseDown += ClearArticleSelection;

        searchQueryBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        searchQueryBox.BackColor = Color.White;
        searchQueryBox.BorderStyle = BorderStyle.FixedSingle;
        searchQueryBox.Font = new Font("Segoe UI", 10F);
        searchQueryBox.ForeColor = Color.FromArgb(32, 32, 32);
        searchQueryBox.Location = new Point(250, 36);
        searchQueryBox.Name = "searchQueryBox";
        searchQueryBox.PlaceholderText = "Exact query used to retrieve these results";
        searchQueryBox.Size = new Size(654, 25);
        searchQueryBox.TabIndex = 1;
        searchQueryBox.MouseDown += ClearArticleSelection;

        importButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        importButton.BackColor = Color.FromArgb(0, 72, 232);
        importButton.Cursor = Cursors.Hand;
        importButton.FlatAppearance.BorderSize = 0;
        importButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 57, 184);
        importButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 91, 235);
        importButton.FlatStyle = FlatStyle.Flat;
        importButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        importButton.ForeColor = Color.White;
        importButton.Location = new Point(924, 36);
        importButton.Name = "importButton";
        importButton.Size = new Size(140, 25);
        importButton.TabIndex = 2;
        importButton.Text = "Import RIS files";
        importButton.UseVisualStyleBackColor = false;
        importButton.Click += ImportFiles;
        importButton.MouseDown += ClearArticleSelection;

        contentPanel.BackColor = Color.White;
        contentPanel.Controls.Add(articleGrid);
        contentPanel.Dock = DockStyle.Fill;
        contentPanel.Location = new Point(0, 88);
        contentPanel.Name = "contentPanel";
        contentPanel.Padding = new Padding(20, 18, 20, 18);
        contentPanel.Size = new Size(1084, 513);
        contentPanel.TabIndex = 2;
        contentPanel.MouseDown += ClearArticleSelection;

        articleGrid.AllowUserToAddRows = false;
        articleGrid.AllowUserToDeleteRows = false;
        articleGrid.AllowUserToOrderColumns = true;
        alternatingRowsStyle.BackColor = Color.FromArgb(249, 249, 249);
        articleGrid.AlternatingRowsDefaultCellStyle = alternatingRowsStyle;
        articleGrid.AutoGenerateColumns = false;
        articleGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        articleGrid.BackgroundColor = Color.White;
        articleGrid.BorderStyle = BorderStyle.FixedSingle;
        articleGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        articleGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        columnHeadersStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        columnHeadersStyle.BackColor = Color.FromArgb(249, 246, 244);
        columnHeadersStyle.ForeColor = Color.FromArgb(55, 55, 55);
        columnHeadersStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        columnHeadersStyle.Padding = new Padding(6, 0, 6, 0);
        columnHeadersStyle.SelectionBackColor = Color.FromArgb(249, 246, 244);
        columnHeadersStyle.SelectionForeColor = Color.FromArgb(55, 55, 55);
        articleGrid.ColumnHeadersDefaultCellStyle = columnHeadersStyle;
        articleGrid.ColumnHeadersHeight = 40;
        articleGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        cellsStyle.BackColor = Color.White;
        cellsStyle.ForeColor = Color.FromArgb(40, 40, 40);
        cellsStyle.Padding = new Padding(6, 0, 6, 0);
        cellsStyle.SelectionBackColor = Color.FromArgb(249, 246, 244);
        cellsStyle.SelectionForeColor = Color.FromArgb(20, 20, 20);
        articleGrid.DefaultCellStyle = cellsStyle;
        articleGrid.Dock = DockStyle.Fill;
        articleGrid.EnableHeadersVisualStyles = false;
        articleGrid.GridColor = Color.FromArgb(225, 225, 225);
        articleGrid.MultiSelect = true;
        articleGrid.Name = "articleGrid";
        articleGrid.ReadOnly = true;
        articleGrid.RowHeadersVisible = false;
        articleGrid.AllowUserToResizeRows = false;
        articleGrid.RowTemplate.Height = 38;
        articleGrid.RowTemplate.Resizable = DataGridViewTriState.False;
        articleGrid.ScrollBars = ScrollBars.Vertical;
        articleGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        articleGrid.CellPainting += PaintCellWithoutFocusRectangle;
        articleGrid.MouseDown += ClearArticleSelectionOutsideRows;
        articleGrid.SelectionChanged += UpdateDeleteButton;
        articleGrid.Columns.AddRange(new DataGridViewColumn[] { searchQueryColumn, sourceColumn, titleColumn, authorsColumn, yearColumn, journalColumn });

        searchQueryColumn.DataPropertyName = "SearchQuery";
        searchQueryColumn.FillWeight = 18F;
        searchQueryColumn.HeaderText = "Search query";
        searchQueryColumn.MinimumWidth = 40;
        searchQueryColumn.Name = "searchQueryColumn";
        searchQueryColumn.ReadOnly = true;
        searchQueryColumn.SortMode = DataGridViewColumnSortMode.Automatic;

        sourceColumn.DataPropertyName = "Source";
        sourceColumn.FillWeight = 10F;
        sourceColumn.HeaderText = "Source";
        sourceColumn.MinimumWidth = 40;
        sourceColumn.Name = "sourceColumn";
        sourceColumn.ReadOnly = true;
        sourceColumn.SortMode = DataGridViewColumnSortMode.Automatic;

        titleColumn.DataPropertyName = "Title";
        titleColumn.FillWeight = 30F;
        titleColumn.HeaderText = "Title";
        titleColumn.MinimumWidth = 40;
        titleColumn.Name = "titleColumn";
        titleColumn.ReadOnly = true;
        titleColumn.SortMode = DataGridViewColumnSortMode.Automatic;

        authorsColumn.DataPropertyName = "Authors";
        authorsColumn.FillWeight = 22F;
        authorsColumn.HeaderText = "Authors";
        authorsColumn.MinimumWidth = 40;
        authorsColumn.Name = "authorsColumn";
        authorsColumn.ReadOnly = true;
        authorsColumn.SortMode = DataGridViewColumnSortMode.Automatic;

        yearColumn.DataPropertyName = "Year";
        yearColumn.FillWeight = 6F;
        yearColumn.HeaderText = "Year";
        yearColumn.MinimumWidth = 40;
        yearColumn.Name = "yearColumn";
        yearColumn.ReadOnly = true;
        yearColumn.SortMode = DataGridViewColumnSortMode.Automatic;

        journalColumn.DataPropertyName = "Journal";
        journalColumn.FillWeight = 14F;
        journalColumn.HeaderText = "Journal";
        journalColumn.MinimumWidth = 40;
        journalColumn.Name = "journalColumn";
        journalColumn.ReadOnly = true;
        journalColumn.SortMode = DataGridViewColumnSortMode.Automatic;

        footerPanel.BackColor = Color.White;
        footerPanel.Controls.Add(statusLabel);
        footerPanel.Controls.Add(exportButton);
        footerPanel.Controls.Add(clearButton);
        footerPanel.Dock = DockStyle.Bottom;
        footerPanel.Location = new Point(0, 601);
        footerPanel.Name = "footerPanel";
        footerPanel.Size = new Size(1084, 60);
        footerPanel.TabIndex = 3;
        footerPanel.MouseDown += ClearArticleSelection;

        statusLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        statusLabel.AutoEllipsis = true;
        statusLabel.ForeColor = Color.FromArgb(100, 100, 100);
        statusLabel.Location = new Point(20, 21);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(768, 20);
        statusLabel.Text = "0 articles saved locally";
        statusLabel.MouseDown += ClearArticleSelection;

        exportButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        exportButton.BackColor = Color.FromArgb(0, 72, 232);
        exportButton.Cursor = Cursors.Hand;
        exportButton.FlatAppearance.BorderSize = 0;
        exportButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 57, 184);
        exportButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 91, 235);
        exportButton.FlatStyle = FlatStyle.Flat;
        exportButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        exportButton.ForeColor = Color.White;
        exportButton.Location = new Point(944, 14);
        exportButton.Name = "exportButton";
        exportButton.Size = new Size(120, 32);
        exportButton.TabIndex = 4;
        exportButton.Text = "Export CSV";
        exportButton.UseVisualStyleBackColor = false;
        exportButton.Click += ExportCsv;
        exportButton.MouseDown += ClearArticleSelection;

        clearButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        clearButton.BackColor = Color.FromArgb(243, 243, 243);
        clearButton.Cursor = Cursors.Hand;
        clearButton.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
        clearButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 232, 232);
        clearButton.FlatStyle = FlatStyle.Flat;
        clearButton.ForeColor = Color.FromArgb(55, 55, 55);
        clearButton.Location = new Point(808, 14);
        clearButton.Name = "clearButton";
        clearButton.Size = new Size(120, 32);
        clearButton.TabIndex = 3;
        clearButton.Text = "Clear collection";
        clearButton.UseVisualStyleBackColor = false;
        clearButton.Click += DeleteArticles;

        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Color.White;
        ClientSize = new Size(1084, 661);
        Controls.Add(contentPanel);
        Controls.Add(footerPanel);
        Controls.Add(inputPanel);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(800, 500);
        Name = "MainForm";
        Padding = new Padding(0);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "RisCollect";
        MouseDown += ClearArticleSelection;
        inputPanel.ResumeLayout(false);
        inputPanel.PerformLayout();
        contentPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)articleGrid).EndInit();
        footerPanel.ResumeLayout(false);
        ResumeLayout(false);
    }
}
