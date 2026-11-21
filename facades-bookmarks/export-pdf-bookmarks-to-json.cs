using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace BookmarkExport
{
    // Simple DTO for JSON serialization
    public class BookmarkInfo
    {
        // Initialize to avoid CS8618 warning for non‑nullable reference types
        public string Title { get; set; } = string.Empty;
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

            // PdfBookmarkEditor does not implement IDisposable, so no using block is needed.
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
            bookmarkEditor.BindPdf(inputPdfPath);

            // Retrieve the top‑level bookmark collection using the facade model API
            Bookmarks topBookmarks = bookmarkEditor.ExtractBookmarks();

            var result = new List<BookmarkInfo>();
            ProcessBookmarks(topBookmarks, 1, result);

            // Serialize to indented JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(result, jsonOptions);

            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Bookmark hierarchy exported to '{outputJsonPath}'.");
        }

        // Recursive traversal of bookmark hierarchy (facade model)
        static void ProcessBookmarks(Bookmarks bookmarks, int level, List<BookmarkInfo> output)
        {
            foreach (Bookmark bm in bookmarks)
            {
                // In the facade model the page number is exposed via the PageNumber property
                int pageNumber = bm.PageNumber;

                output.Add(new BookmarkInfo
                {
                    Title = bm.Title,
                    Level = level,
                    PageNumber = pageNumber
                });

                // Child bookmarks are accessed through the ChildItems collection
                if (bm.ChildItems != null && bm.ChildItems.Count > 0)
                {
                    ProcessBookmarks(bm.ChildItems, level + 1, output);
                }
            }
        }
    }
}
