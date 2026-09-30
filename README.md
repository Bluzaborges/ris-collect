<p align="center">
  <img src="images/ris-collect-logo.svg" alt="RisCollect logo" width="96">
</p>

# RisCollect

RisCollect is a Windows desktop application for consolidating academic article metadata exported in RIS format. It reads common RIS tags, lets researchers identify the source and exact query used for each import, and combines results from several files in a single reviewable table.

Multiple files can be imported at once to accommodate platforms that limit RIS exports by result page. The collection is persisted locally in SQLite through Entity Framework Core and can be exported as a UTF-8 CSV file for use in spreadsheet and data-analysis software.

## Install and run on Windows

1. Download the ZIP from the latest release and extract all its files into one folder.
2. Open the extracted folder and run `RisCollect.exe`.

## Build

```powershell
dotnet build -c Release
```

To create a distributable folder:

```powershell
dotnet publish src/RisCollect -c Release -o publish
```

## Usage

Start the application from the repository:

```powershell
dotnet run --project src/RisCollect
```

In the application:

1. Enter the database or platform from which the RIS files were exported.
2. Enter the search query exactly as it was used on the platform.
3. Select **Import RIS files** and choose one or more `.ris` files.
4. Review the consolidated articles in the table.
5. Select **Export CSV** to save the current collection.

The CSV includes the search query, source, bibliographic metadata, link, abstract, and keywords for each article.

## Local storage

Imported articles are saved automatically in a local SQLite database and restored when the application starts. No database server, account, or manual configuration is required.

The database is stored at:

```text
%LocalAppData%\RisCollect\riscollect.sqlite
```

## Deduplication

RisCollect identifies duplicates using the normalized combination of:

- Article title
- Search query
- Source platform
- Journal

Only entries with the same values in all four fields are replaced. This preserves the same article when it belongs to another search query, source, or journal.

## License

This project is licensed under the [MIT License](LICENSE).
