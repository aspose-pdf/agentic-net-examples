using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades; // PdfBookmarkEditor and Bookmark classes

namespace BookmarkJsonExport
{
    // Simple POCO to represent a bookmark node for JSON serialization
    public class BookmarkNode
    {
        public string Title { get; set; }
        public int? PageNumber { get; set; } // null if not set
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

            // Initialize the bookmark editor and bind the PDF
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(inputPdfPath);

            // Extract all bookmarks (recursive)
            Bookmarks extracted = editor.ExtractBookmarks();

            // Convert to a hierarchy of BookmarkNode objects
            List<BookmarkNode> rootNodes = new List<BookmarkNode>();
            foreach (Bookmark bm in extracted)
            {
                rootNodes.Add(ConvertToNode(bm));
            }

            // Serialize hierarchy to pretty‑printed JSON
            string json = JsonSerializer.Serialize(rootNodes, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Write JSON to file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Bookmarks exported to '{outputJsonPath}'.");

            // Clean up the editor
            editor.Close();
        }

        // Recursive conversion from Aspose.Pdf.Facades.Bookmark to our POCO
        private static BookmarkNode ConvertToNode(Bookmark bm)
        {
            BookmarkNode node = new BookmarkNode {
                Title = bm.Title,
                PageNumber = bm.PageNumber > 0 ? (int?)bm.PageNumber : null
            };

            // ChildItems holds nested bookmarks; process them recursively
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                foreach (Bookmark child in bm.ChildItems)
                {
                    node.Children.Add(ConvertToNode(child));
                }
            }

            return node;
        }
    }
}