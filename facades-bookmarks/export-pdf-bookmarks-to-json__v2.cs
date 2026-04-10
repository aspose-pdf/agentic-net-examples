using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
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
                Console.Error.WriteLine($"File not found: {inputPdfPath}");
                return;
            }

            // Load the PDF and work with its bookmarks using PdfBookmarkEditor
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                editor.BindPdf(inputPdfPath);

                // Extract all bookmarks (including nested ones)
                Bookmarks rootBookmarks = editor.ExtractBookmarks();

                // Flatten the hierarchy while preserving level information
                List<BookmarkInfo> flatList = new List<BookmarkInfo>();
                TraverseBookmarks(rootBookmarks, 1, flatList);

                // Serialize to pretty‑printed JSON
                string json = JsonSerializer.Serialize(
                    flatList,
                    new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(outputJsonPath, json);
                Console.WriteLine($"Bookmarks exported to '{outputJsonPath}'.");
            }
        }

        // Recursively walk the bookmark tree
        private static void TraverseBookmarks(Bookmarks bookmarks, int level, List<BookmarkInfo> result)
        {
            foreach (Bookmark bm in bookmarks)
            {
                result.Add(new BookmarkInfo
                {
                    Title = bm.Title,
                    Level = level,
                    PageNumber = bm.PageNumber
                });

                // ChildItem holds the nested bookmarks (if any)
                if (bm.ChildItem != null && bm.ChildItem.Count > 0)
                {
                    TraverseBookmarks(bm.ChildItem, level + 1, result);
                }
            }
        }
    }
}