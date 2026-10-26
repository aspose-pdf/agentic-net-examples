using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "bookmarks.csv";
        const string outputPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, so no using block is required
        var bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(pdfPath);

        // Process each line of the CSV file
        foreach (var line in File.ReadLines(csvPath))
        {
            // Skip empty or whitespace‑only lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Expected CSV format: title,level,destination
            // Destination is interpreted as a 1‑based page number
            var parts = line.Split(',');

            if (parts.Length < 3)
            {
                Console.Error.WriteLine($"Invalid line (expected 3 columns): {line}");
                continue;
            }

            var title = parts[0].Trim();

            // Parse bookmark level (must be >= 1)
            if (!int.TryParse(parts[1].Trim(), out int level) || level < 1)
            {
                Console.Error.WriteLine($"Invalid level in line: {line}");
                continue;
            }

            // Parse destination page number (must be >= 1)
            if (!int.TryParse(parts[2].Trim(), out int destination) || destination < 1)
            {
                Console.Error.WriteLine($"Invalid destination (page number) in line: {line}");
                continue;
            }

            // Build a Bookmark object and add it via CreateBookmarks(Bookmark)
            var bookmark = new Bookmark
            {
                Title = title,
                PageNumber = destination,
                Level = level,
                Action = "GoTo"
            };

            // Add the bookmark to the PDF
            bookmarkEditor.CreateBookmarks(bookmark);
        }

        // Save the modified PDF with the new bookmarks
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmarks imported and saved to '{outputPath}'.");
    }
}
