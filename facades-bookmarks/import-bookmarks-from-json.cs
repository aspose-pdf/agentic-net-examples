using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

// Model that matches the JSON structure
public class BookmarkEntry
{
    // Mark as required to satisfy non‑nullable analysis
    public required string Title { get; set; }
    public int Page { get; set; }
    // Optional nested bookmarks – not required for the basic example
    public List<BookmarkEntry>? Children { get; set; }
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "bookmarked.pdf";
        const string jsonPath = "bookmarks.json";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load bookmark definitions from JSON
        string jsonContent = File.ReadAllText(jsonPath);
        List<BookmarkEntry> bookmarks = JsonSerializer.Deserialize<List<BookmarkEntry>>(jsonContent);

        // Use PdfBookmarkEditor (facade) to add bookmarks
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPdfPath);

            foreach (BookmarkEntry entry in bookmarks)
            {
                // Create a Facade Bookmark and set its properties
                Bookmark pdfBookmark = new Bookmark
                {
                    Title = entry.Title,
                    PageNumber = entry.Page // 1‑based page index
                };

                // Add the bookmark as a top‑level entry
                bookmarkEditor.CreateBookmarks(pdfBookmark);
            }

            // Save the PDF with the new bookmarks
            bookmarkEditor.Save(outputPdfPath);
        }

        Console.WriteLine($"Bookmarks successfully added. Output saved to '{outputPdfPath}'.");
    }
}
