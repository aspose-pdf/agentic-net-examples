using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace BookmarkExportExample
{
    // Simple DTO for JSON serialization
    public class BookmarkInfo
    {
        public string Title { get; set; }
        public int Level { get; set; }
        public int PageNumber { get; set; }
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

            // Extract bookmarks using PdfBookmarkEditor (facade API)
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                editor.BindPdf(inputPdfPath);

                // Get all bookmarks (recursive hierarchy)
                var allBookmarks = editor.ExtractBookmarks();

                var flatList = new List<BookmarkInfo>();
                TraverseBookmarks(allBookmarks, 1, flatList);

                // Serialize to JSON with indentation for readability
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(flatList, jsonOptions);

                File.WriteAllText(outputJsonPath, json);
                Console.WriteLine($"Bookmarks exported to '{outputJsonPath}'.");
            }
        }

        // Recursively walk the bookmark tree, recording title, level and page number
        private static void TraverseBookmarks(Aspose.Pdf.Facades.Bookmarks bookmarks, int level, List<BookmarkInfo> result)
        {
            foreach (Aspose.Pdf.Facades.Bookmark bm in bookmarks)
            {
                result.Add(new BookmarkInfo
                {
                    Title = bm.Title,
                    Level = level,
                    PageNumber = bm.PageNumber
                });

                // If the bookmark has children, recurse with increased level
                if (bm.ChildItem != null && bm.ChildItem.Count > 0)
                {
                    TraverseBookmarks(bm.ChildItem, level + 1, result);
                }
            }
        }
    }
}