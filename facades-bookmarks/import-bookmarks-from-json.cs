using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

// JSON structure that represents a bookmark entry.
// It can contain nested children to form a hierarchy.
public class JsonBookmark
{
    public string Title { get; set; }
    public int PageNumber { get; set; }
    public List<JsonBookmark> Children { get; set; } = new List<JsonBookmark>();
}

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // PDF to which bookmarks will be added
        const string jsonPath       = "bookmarks.json"; // JSON file containing bookmark definitions
        const string outputPdfPath  = "output_with_bookmarks.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load and deserialize the JSON file into a list of objects.
        // ------------------------------------------------------------
        List<JsonBookmark> jsonBookmarks;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            jsonBookmarks = JsonSerializer.Deserialize<List<JsonBookmark>>(jsonContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read/parse JSON: {ex.Message}");
            return;
        }

        if (jsonBookmarks == null || jsonBookmarks.Count == 0)
        {
            Console.WriteLine("No bookmarks found in JSON.");
            return;
        }

        // ------------------------------------------------------------
        // 2. Convert the JSON objects to Aspose.Pdf.Facades.Bookmark objects.
        // ------------------------------------------------------------
        List<Bookmark> aspBookmarks = new List<Bookmark>();
        foreach (var jb in jsonBookmarks)
        {
            aspBookmarks.Add(ConvertToAsposeBookmark(jb));
        }

        // ------------------------------------------------------------
        // 3. Bind the PDF, add the bookmarks, and save the result.
        // ------------------------------------------------------------
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        try
        {
            // Load the PDF document.
            editor.BindPdf(inputPdfPath);

            // Add each top‑level bookmark (the method handles nested children).
            foreach (var bm in aspBookmarks)
            {
                editor.CreateBookmarks(bm);
            }

            // Persist the changes.
            editor.Save(outputPdfPath);
            Console.WriteLine($"Bookmarks imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
        finally
        {
            // Release resources held by the facade.
            editor.Close();
        }
    }

    // Recursively maps a JsonBookmark to an Aspose.Pdf.Facades.Bookmark.
    private static Bookmark ConvertToAsposeBookmark(JsonBookmark jsonBm)
    {
        // Create the bookmark and set required properties.
        Bookmark bm = new Bookmark
        {
            Title = jsonBm.Title,
            PageNumber = jsonBm.PageNumber,
            Action = "GoTo" // Standard action to navigate to the page.
        };

        // If there are child bookmarks, convert them and attach.
        if (jsonBm.Children != null && jsonBm.Children.Count > 0)
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
}