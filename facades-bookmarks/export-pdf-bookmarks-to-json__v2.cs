using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace BookmarkExportExample
{
    // Simple POCO to hold bookmark information for JSON serialization
    public class BookmarkNode
    {
        public string Title { get; set; }
        public int Level { get; set; }
        public int PageNumber { get; set; }
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
                Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
                return;
            }

            // Use PdfBookmarkEditor to work with bookmarks (facade API)
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Bind the PDF file to the editor
                editor.BindPdf(inputPdfPath);

                // Extract all bookmarks (hierarchical collection)
                var topBookmarks = editor.ExtractBookmarks();

                // Convert the Aspose bookmark hierarchy to our serializable model
                List<BookmarkNode> jsonBookmarks = new List<BookmarkNode>();
                foreach (var bm in topBookmarks)
                {
                    jsonBookmarks.Add(ConvertToNode(bm, 1));
                }

                // Serialize to JSON with indentation for readability
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(jsonBookmarks, jsonOptions);

                // Write JSON to the output file
                File.WriteAllText(outputJsonPath, json);
                Console.WriteLine($"Bookmarks exported to '{outputJsonPath}'.");
            }
        }

        // Recursive conversion from Aspose Bookmark to BookmarkNode
        private static BookmarkNode ConvertToNode(Aspose.Pdf.Facades.Bookmark aspBookmark, int level)
        {
            BookmarkNode node = new BookmarkNode {
                Title = aspBookmark.Title,
                Level = level,
                PageNumber = aspBookmark.PageNumber
            };

            // ChildItem holds nested bookmarks (may be null)
            if (aspBookmark.ChildItem != null && aspBookmark.ChildItem.Count > 0)
            {
                foreach (var child in aspBookmark.ChildItem)
                {
                    node.Children.Add(ConvertToNode(child, level + 1));
                }
            }

            return node;
        }
    }
}