using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace BookmarkExport
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

            try
            {
                // Use the facade model for bookmark handling
                using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
                {
                    bookmarkEditor.BindPdf(inputPdfPath);

                    // Extract the top‑level bookmark collection using the correct API
                    Bookmarks rootBookmarks = bookmarkEditor.ExtractBookmarks();

                    // Flatten the hierarchy into a list with level information
                    List<BookmarkInfo> flatList = new List<BookmarkInfo>();
                    ExtractBookmarks(rootBookmarks, 1, flatList); // level starts at 1

                    // Serialize to pretty‑printed JSON
                    JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(flatList, jsonOptions);
                    File.WriteAllText(outputJsonPath, json);
                }

                Console.WriteLine($"Bookmark hierarchy exported to '{outputJsonPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }

        // Recursively walk the bookmark tree using the facade model types
        private static void ExtractBookmarks(Bookmarks bookmarks, int level, List<BookmarkInfo> list)
        {
            foreach (Bookmark bm in bookmarks)
            {
                // PageNumber is always available in the facade model
                int pageNumber = bm.PageNumber;

                list.Add(new BookmarkInfo
                {
                    Title = bm.Title,
                    Level = level,
                    PageNumber = pageNumber
                });

                // Process child bookmarks, if any
                if (bm.ChildItems != null && bm.ChildItems.Count > 0)
                {
                    ExtractBookmarks(bm.ChildItems, level + 1, list);
                }
            }
        }
    }
}
