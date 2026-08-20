using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;   // Bookmark, Bookmarks, PdfBookmarkEditor

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";
        const string jsonPath     = "bookmarks.json";
        const string outputPath   = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON not found: {jsonPath}");
            return;
        }

        // Load JSON and deserialize into a list of bookmark definitions
        string jsonContent = File.ReadAllText(jsonPath);
        List<JsonBookmark> jsonBookmarks = JsonSerializer.Deserialize<List<JsonBookmark>>(jsonContent);

        // Initialize the bookmark editor and bind the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(pdfPath);

        // Optional: remove any existing bookmarks
        editor.DeleteBookmarks();

        // Convert each JSON entry to an Aspose.Pdf.Facades.Bookmark and add it
        foreach (JsonBookmark jb in jsonBookmarks)
        {
            Bookmark bm = ConvertToBookmark(jb);
            editor.CreateBookmarks(bm);
        }

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();   // releases resources held by the facade

        Console.WriteLine($"Bookmarks imported and saved to '{outputPath}'.");
    }

    // Recursively maps a JsonBookmark to an Aspose.Pdf.Facades.Bookmark
    static Bookmark ConvertToBookmark(JsonBookmark source)
    {
        Bookmark bm = new Bookmark
        {
            Title      = source.Title,
            PageNumber = source.PageNumber,
            Action     = "GoTo"
        };

        if (source.Children != null && source.Children.Count > 0)
        {
            Bookmarks childCollection = new Bookmarks();
            foreach (JsonBookmark child in source.Children)
            {
                childCollection.Add(ConvertToBookmark(child));
            }
            bm.ChildItem = childCollection;
        }

        return bm;
    }

    // POCO matching the expected JSON structure
    class JsonBookmark
    {
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public List<JsonBookmark> Children { get; set; }
    }
}