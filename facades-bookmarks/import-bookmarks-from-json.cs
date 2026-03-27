using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace BookmarkImportExample
{
    public class BookmarkInfo
    {
        public string Title { get; set; }
        public int PageNumber { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string jsonPath = "bookmarks.json";
            const string outputPdfPath = "output.pdf";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"PDF file not found: {inputPdfPath}");
                return;
            }

            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"JSON file not found: {jsonPath}");
                return;
            }

            // Read and deserialize JSON file containing an array of { "Title": "...", "PageNumber": n }
            List<BookmarkInfo> bookmarkList;
            using (FileStream jsonStream = File.OpenRead(jsonPath))
            {
                bookmarkList = JsonSerializer.Deserialize<List<BookmarkInfo>>(jsonStream);
            }

            if (bookmarkList == null || bookmarkList.Count == 0)
            {
                Console.Error.WriteLine("No bookmarks found in JSON file.");
                return;
            }

            // Load the source PDF
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialise the bookmark editor with the loaded document
                PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor(pdfDocument);

                // Add each bookmark from the JSON data
                foreach (BookmarkInfo info in bookmarkList)
                {
                    Bookmark bookmark = new Bookmark();
                    bookmark.Title = info.Title;
                    bookmark.PageNumber = info.PageNumber;
                    // Create the bookmark in the document
                    bookmarkEditor.CreateBookmarks(bookmark);
                }

                // Save the modified PDF
                bookmarkEditor.Save(outputPdfPath);
                // Close the editor (optional, releases resources)
                bookmarkEditor.Close();
            }

            Console.WriteLine($"Bookmarks imported and saved to '{outputPdfPath}'.");
        }
    }
}