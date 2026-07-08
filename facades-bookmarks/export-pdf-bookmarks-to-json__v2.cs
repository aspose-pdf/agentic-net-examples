using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace BookmarkExportExample
{
    // Simple DTO that will be serialized to JSON
    public class BookmarkInfo
    {
        public string Title { get; set; }
        public int Level { get; set; }
        public int PageNumber { get; set; }
        public List<BookmarkInfo> Children { get; set; } = new List<BookmarkInfo>();
    }

    class Program
    {
        // Recursively converts Aspose.Pdf.Facades.Bookmark objects to BookmarkInfo DTOs
        static BookmarkInfo ConvertBookmark(Bookmark bm, int level)
        {
            BookmarkInfo info = new BookmarkInfo {
                Title = bm.Title,
                Level = level,
                PageNumber = bm.PageNumber
            };

            // ChildItem holds the collection of child bookmarks (may be null)
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                foreach (Bookmark child in bm.ChildItem)
                {
                    info.Children.Add(ConvertBookmark(child, level + 1));
                }
            }

            return info;
        }

        static void Main()
        {
            const string inputPdf = "input.pdf";
            const string outputJson = "bookmarks.json";

            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input file not found: {inputPdf}");
                return;
            }

            try
            {
                // Load the PDF document using Aspose.Pdf (lifecycle rule)
                using (Document doc = new Document(inputPdf))
                {
                    // Use PdfBookmarkEditor from Aspose.Pdf.Facades to work with bookmarks
                    using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
                    {
                        editor.BindPdf(doc); // bind the loaded document

                        // Extract all bookmarks (recursive hierarchy)
                        Bookmarks rootBookmarks = editor.ExtractBookmarks();

                        // Convert to DTO list
                        var bookmarkList = new List<BookmarkInfo>();
                        foreach (Bookmark bm in rootBookmarks)
                        {
                            bookmarkList.Add(ConvertBookmark(bm, 1)); // top‑level starts at 1
                        }

                        // Serialize to JSON with indentation for readability
                        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                        string json = JsonSerializer.Serialize(bookmarkList, jsonOptions);

                        // Write JSON to file (standard .NET I/O)
                        File.WriteAllText(outputJson, json);
                    }

                    // No need to save the PDF because we only read bookmarks
                }

                Console.WriteLine($"Bookmarks exported to '{outputJson}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}