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
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, so we just instantiate it
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Load the PDF to which bookmarks will be added
        bookmarkEditor.BindPdf(pdfPath);

        // Process each line of the CSV file
        foreach (string line in File.ReadLines(csvPath))
        {
            // Skip empty or whitespace‑only lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Expected CSV format: title,level,destination
            // Split into exactly three parts (title may contain commas if quoted – omitted for brevity)
            string[] parts = line.Split(new[] { ',' }, 3);
            if (parts.Length != 3)
            {
                Console.Error.WriteLine($"Invalid CSV line (expected 3 columns): {line}");
                continue;
            }

            string title = parts[0].Trim();

            // Parse bookmark level (must be a positive integer)
            if (!int.TryParse(parts[1].Trim(), out int level) || level < 1)
            {
                Console.Error.WriteLine($"Invalid level value '{parts[1]}' in line: {line}");
                continue;
            }

            string destination = parts[2].Trim();

            // Build a Facade Bookmark object
            Bookmark bm = new Bookmark
            {
                Title = title,
                Level = level,
                Action = "GoTo" // required for navigation
            };

            // Determine whether the destination is a page number or a named destination
            if (int.TryParse(destination, out int pageNumber) && pageNumber > 0)
            {
                bm.PageNumber = pageNumber; // pages are 1‑based
            }
            else
            {
                bm.Destination = destination; // named destination defined elsewhere in the PDF
            }

            // Add the bookmark to the PDF using the correct PdfBookmarkEditor method
            bookmarkEditor.CreateBookmarks(bm);
        }

        // Save the PDF with the newly added bookmarks
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmarks imported successfully to '{outputPath}'.");
    }
}
