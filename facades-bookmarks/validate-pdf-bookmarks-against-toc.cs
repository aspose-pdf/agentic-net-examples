using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // needed for TextFragmentAbsorber and TextExtractionOptions

class BookmarkConsistencyChecker
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string reportPath   = "bookmark_consistency_report.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (required for page count, etc.)
        using (Document doc = new Document(inputPdfPath))
        {
            // Bind the PDF to the bookmark editor (Facades API)
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
            bookmarkEditor.BindPdf(inputPdfPath);

            // Retrieve all bookmarks (outline entries) using the Facade API
            // NOTE: The Facade provides ExtractBookmarks(), not GetBookmarks().
            Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

            // Extract bookmark titles and their destination page numbers
            var bookmarkList = new List<(string Title, int PageNumber)>();
            foreach (Bookmark bm in bookmarks)
            {
                // In the Facade model the page number is exposed via PageNumber (1‑based).
                int pageNum = bm.PageNumber;
                bookmarkList.Add((bm.Title, pageNum));
            }

            // ------------------------------------------------------------
            // Generate a simple Table of Contents (TOC) for comparison.
            // In a real scenario this could be built from headings or
            // another source. Here we simulate it by extracting the
            // first line of each page as a TOC entry.
            // ------------------------------------------------------------
            var tocList = new List<(string Title, int PageNumber)>();
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];
                // Use TextFragmentAbsorber (the correct class) to get page text
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                page.Accept(absorber);
                string pageText = absorber.Text?.Trim() ?? string.Empty;
                string[] lines = pageText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length > 0)
                {
                    string firstLine = lines[0];
                    if (!string.IsNullOrEmpty(firstLine))
                    {
                        tocList.Add((firstLine, i));
                    }
                }
            }

            // ------------------------------------------------------------
            // Compare bookmark list against generated TOC
            // ------------------------------------------------------------
            var mismatches = new List<string>();
            foreach (var bm in bookmarkList)
            {
                // Find matching TOC entry by title (case‑insensitive)
                var match = tocList.Find(t => string.Equals(t.Title, bm.Title, StringComparison.OrdinalIgnoreCase));
                if (match.Equals(default((string, int)))) // no match found
                {
                    mismatches.Add($"Bookmark \"{bm.Title}\" not found in TOC.");
                }
                else if (match.PageNumber != bm.PageNumber)
                {
                    mismatches.Add($"Page mismatch for \"{bm.Title}\": Bookmark page {bm.PageNumber}, TOC page {match.PageNumber}.");
                }
            }

            // ------------------------------------------------------------
            // Write the consistency report
            // ------------------------------------------------------------
            using (StreamWriter writer = new StreamWriter(reportPath, false))
            {
                writer.WriteLine("Bookmark Consistency Report");
                writer.WriteLine($"Generated on: {DateTime.Now}");
                writer.WriteLine();

                if (mismatches.Count == 0)
                {
                    writer.WriteLine("All bookmarks match the generated Table of Contents.");
                }
                else
                {
                    writer.WriteLine("Mismatches found:");
                    foreach (string line in mismatches)
                    {
                        writer.WriteLine("- " + line);
                    }
                }
            }

            Console.WriteLine($"Consistency check completed. Report saved to \"{reportPath}\".");
        }
    }
}
