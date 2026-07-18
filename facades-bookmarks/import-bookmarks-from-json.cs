using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;                     // Document
using Aspose.Pdf.Facades;            // PdfBookmarkEditor, Bookmark

class Program
{
    // Represents a single bookmark entry in the JSON file.
    private class BookmarkEntry
    {
        public string Title { get; set; }
        public int Page { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // Source PDF
        const string jsonPath      = "bookmarks.json"; // JSON file with bookmark data
        const string outputPdfPath = "output_with_bookmarks.pdf";

        // Validate files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON not found: {jsonPath}");
            return;
        }

        // Deserialize JSON into a list of bookmark entries.
        List<BookmarkEntry> entries;
        try
        {
            string json = File.ReadAllText(jsonPath);
            entries = JsonSerializer.Deserialize<List<BookmarkEntry>>(json);
            if (entries == null)
                throw new InvalidOperationException("Deserialized bookmark list is null.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read JSON: {ex.Message}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Initialize the bookmark editor and bind it to the opened document.
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                editor.BindPdf(doc);

                // Add each bookmark from the JSON data.
                foreach (BookmarkEntry entry in entries)
                {
                    // Ensure page number is within the document range (Aspose.Pdf uses 1‑based indexing).
                    if (entry.Page < 1 || entry.Page > doc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Skipping invalid page {entry.Page} for title \"{entry.Title}\".");
                        continue;
                    }

                    // Create a bookmark that points to the specified page.
                    editor.CreateBookmarkOfPage(entry.Title, entry.Page);
                }

                // Save the modified PDF via the editor (which writes the bound document).
                editor.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Bookmarks imported and saved to '{outputPdfPath}'.");
    }
}