using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks
using Aspose.Pdf;           // Document (if needed)

// Model that matches the expected JSON structure
public class JsonBookmark
{
    public string Title { get; set; }
    public int PageNumber { get; set; }
    public JsonBookmark[] Children { get; set; }
}

public class Program
{
    // Recursively converts a JsonBookmark into an Aspose.Pdf.Facades.Bookmark
    private static Bookmark ConvertToAsposeBookmark(JsonBookmark jsonBm)
    {
        Bookmark bm = new Bookmark {
            Title = jsonBm.Title,
            PageNumber = jsonBm.PageNumber,
            Action = "GoTo"               // standard action for page navigation
        };

        if (jsonBm.Children != null && jsonBm.Children.Length > 0)
        {
            Bookmarks childCollection = new Bookmarks();
            foreach (var childJson in jsonBm.Children)
            {
                childCollection.Add(ConvertToAsposeBookmark(childJson));
            }
            bm.ChildItem = childCollection;
        }

        return bm;
    }

    public static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string jsonFilePath   = "bookmarks.json";
        const string outputPdfPath  = "output.pdf";

        // Basic validation
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(jsonFilePath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonFilePath}");
            return;
        }

        // Read and deserialize the JSON file
        string jsonContent = File.ReadAllText(jsonFilePath);
        JsonBookmark[] jsonBookmarks = JsonSerializer.Deserialize<JsonBookmark[]>(jsonContent);

        // Ensure we have data to work with
        if (jsonBookmarks == null || jsonBookmarks.Length == 0)
        {
            Console.Error.WriteLine("No bookmarks found in the JSON file.");
            return;
        }

        // Use PdfBookmarkEditor (facade) to bind the PDF and add bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF file
            editor.BindPdf(inputPdfPath);

            // Build a root bookmark that will contain all top‑level bookmarks
            Bookmark rootBookmark = new Bookmark {
                Title = "Root",          // dummy title; not visible in the final outline
                ChildItem = new Bookmarks()
            };

            // Convert each JSON entry to an Aspose bookmark and attach it
            foreach (var jsonBm in jsonBookmarks)
            {
                rootBookmark.ChildItem.Add(ConvertToAsposeBookmark(jsonBm));
            }

            // Add the constructed hierarchy to the document
            editor.CreateBookmarks(rootBookmark);

            // Save the updated PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Bookmarks imported successfully. Output saved to '{outputPdfPath}'.");
    }
}