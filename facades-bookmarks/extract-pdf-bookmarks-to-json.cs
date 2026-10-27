using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

// Model representing a bookmark node for JSON serialization
public class BookmarkNode
{
    public string? Title { get; set; }
    public string? Action { get; set; }
    public int? DestinationPage { get; set; }
    public List<BookmarkNode> Children { get; set; } = new List<BookmarkNode>();
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "bookmarks.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document using Aspose.Pdf.Document
        Document pdfDocument = new Document(inputPdfPath);

        // In recent Aspose.Pdf versions the bookmark hierarchy is exposed via the Outlines collection
        var topBookmarks = pdfDocument.Outlines; // OutlineCollection implements IEnumerable

        // Convert the Aspose outline hierarchy to our plain POCO hierarchy
        List<BookmarkNode> jsonTree = ConvertBookmarks(topBookmarks);

        // Serialize to indented JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(jsonTree, jsonOptions);

        // Write JSON to file
        File.WriteAllText(outputJsonPath, json);

        Console.WriteLine($"Bookmarks exported to '{outputJsonPath}'.");
    }

    // Recursively transforms any IEnumerable (OutlineCollection or its children) into List<BookmarkNode>
    private static List<BookmarkNode> ConvertBookmarks(object? collectionObj)
    {
        var list = new List<BookmarkNode>();
        if (collectionObj == null) return list;

        var enumerable = collectionObj as System.Collections.IEnumerable;
        if (enumerable == null) return list;

        foreach (var item in enumerable)
        {
            // Use dynamic to avoid compile‑time coupling with Aspose's concrete Outline type
            dynamic aspOutline = item;
            var node = new BookmarkNode
            {
                Title = aspOutline.Title as string,
                Action = aspOutline.Action as string,
                DestinationPage = aspOutline.DestinationPage > 0 ? (int?)aspOutline.DestinationPage : null,
                Children = ConvertBookmarks(aspOutline.Children)
            };
            list.Add(node);
        }
        return list;
    }
}
