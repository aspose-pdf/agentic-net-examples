using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // needed for Destination types (e.g., XYZExplicitDestination)

class Program
{
    // Simple DTO for JSON output
    private class BookmarkInfo
    {
        public string Title { get; set; }
        public string Destination { get; set; }
    }

    static void Main()
    {
        const string pdfPath = "sample.pdf";
        const string jsonPath = "bookmarks.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            var bookmarks = new List<BookmarkInfo>();

            // The root outline collection is doc.Outlines (type OutlineItemCollection).
            // Iterate each top‑level bookmark and recurse.
            foreach (OutlineItemCollection topItem in doc.Outlines)
            {
                ExtractBookmarks(topItem, bookmarks);
            }

            // Serialize the list to pretty‑printed JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(bookmarks, jsonOptions);

            // Write JSON to file (does not modify the PDF)
            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"Bookmarks exported to '{jsonPath}'.");
    }

    // Recursive helper to flatten the outline hierarchy
    private static void ExtractBookmarks(OutlineItemCollection item, List<BookmarkInfo> list)
    {
        string destination = string.Empty;
        if (item.Destination != null)
        {
            // Use the destination's string representation for simplicity.
            destination = item.Destination.ToString();
        }

        list.Add(new BookmarkInfo
        {
            Title = item.Title,
            Destination = destination
        });

        // Process child items (the item itself acts as a collection of its children).
        foreach (OutlineItemCollection child in item)
        {
            ExtractBookmarks(child, list);
        }
    }
}
