using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades; // PdfBookmarkEditor, Bookmark, Bookmarks

// Simple POCO that matches the desired JSON structure
public class JsonBookmark
{
    public string Title { get; set; }
    public int? PageNumber { get; set; }          // nullable because a bookmark may not have a page number
    public List<JsonBookmark> Children { get; set; } = new List<JsonBookmark>();
}

class Program
{
    // Recursively converts Aspose.Pdf.Facades.Bookmark to our JsonBookmark model
    private static JsonBookmark ConvertBookmark(Bookmark bm)
    {
        JsonBookmark jsonBm = new JsonBookmark {
            Title = bm.Title,
            PageNumber = bm.PageNumber > 0 ? (int?)bm.PageNumber : null
        };

        // ChildItems holds the nested bookmarks (may be null)
        if (bm.ChildItems != null && bm.ChildItems.Count > 0)
        {
            foreach (Bookmark child in bm.ChildItems)
            {
                jsonBm.Children.Add(ConvertBookmark(child));
            }
        }

        return jsonBm;
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "bookmarks.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Use PdfBookmarkEditor to extract bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdfPath);                     // load PDF
            Bookmarks extracted = editor.ExtractBookmarks(); // get all levels

            // Convert the Aspose bookmark collection to a list of POCOs
            var jsonBookmarks = new List<JsonBookmark>();
            foreach (Bookmark bm in extracted)
            {
                jsonBookmarks.Add(ConvertBookmark(bm));
            }

            // Serialize to pretty‑printed JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jsonBookmarks, jsonOptions);
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Bookmarks exported to '{outputJsonPath}'.");
    }
}