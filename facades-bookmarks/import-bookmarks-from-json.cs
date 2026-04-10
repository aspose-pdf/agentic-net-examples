using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace BookmarkImportExample
{
    // Represents the JSON structure for a bookmark entry.
    public class BookmarkNode
    {
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public List<BookmarkNode> Children { get; set; }
    }

    class Program
    {
        // Recursively converts a BookmarkNode (JSON) to an Aspose.Pdf.Facades.Bookmark.
        static Bookmark ConvertToAsposeBookmark(BookmarkNode node)
        {
            Bookmark aspBookmark = new Bookmark {
                Title = node.Title,
                PageNumber = node.PageNumber,
                Action = "GoTo" // standard action for page navigation
            };

            if (node.Children != null && node.Children.Count > 0)
            {
                Bookmarks childCollection = new Bookmarks();
                foreach (var childNode in node.Children)
                {
                    childCollection.Add(ConvertToAsposeBookmark(childNode));
                }
                aspBookmark.ChildItem = childCollection;
            }

            return aspBookmark;
        }

        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string outputPdfPath = "output_with_bookmarks.pdf";
            const string jsonBookmarksPath = "bookmarks.json";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
                return;
            }

            if (!File.Exists(jsonBookmarksPath))
            {
                Console.Error.WriteLine($"JSON file not found: {jsonBookmarksPath}");
                return;
            }

            // Deserialize JSON into a list of bookmark nodes.
            List<BookmarkNode> rootNodes;
            try
            {
                string jsonContent = File.ReadAllText(jsonBookmarksPath);
                rootNodes = JsonSerializer.Deserialize<List<BookmarkNode>>(jsonContent);
                if (rootNodes == null)
                {
                    Console.Error.WriteLine("Failed to parse JSON bookmarks.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error reading JSON: {ex.Message}");
                return;
            }

            // Use PdfBookmarkEditor facade to bind the PDF and import bookmarks.
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                editor.BindPdf(inputPdfPath);

                // Convert each root node and add it to the document.
                foreach (var node in rootNodes)
                {
                    Bookmark aspBookmark = ConvertToAsposeBookmark(node);
                    editor.CreateBookmarks(aspBookmark);
                }

                // Save the updated PDF.
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Bookmarks imported and saved to '{outputPdfPath}'.");
        }
    }
}