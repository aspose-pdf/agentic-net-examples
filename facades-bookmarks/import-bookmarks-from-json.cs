using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace BookmarkImportExample
{
    // Model that matches the expected JSON structure
    public class JsonBookmark
    {
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public List<JsonBookmark> Children { get; set; }
    }

    class Program
    {
        // Recursively converts a JsonBookmark into an Aspose.Pdf.Facades.Bookmark
        static Bookmark ConvertToAsposeBookmark(JsonBookmark source)
        {
            // Create the Aspose bookmark and set basic properties
            Bookmark bm = new Bookmark
            {
                Title = source.Title,
                PageNumber = source.PageNumber,
                // The default action for a page bookmark is "GoTo"
                Action = "GoTo"
            };

            // If the source has child bookmarks, convert them and attach
            if (source.Children != null && source.Children.Count > 0)
            {
                Bookmarks childCollection = new Bookmarks();
                foreach (JsonBookmark child in source.Children)
                {
                    childCollection.Add(ConvertToAsposeBookmark(child));
                }
                bm.ChildItem = childCollection;
            }

            return bm;
        }

        static void Main()
        {
            const string inputPdfPath  = "input.pdf";
            const string jsonPath      = "bookmarks.json";
            const string outputPdfPath = "output.pdf";

            // Validate input files
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

            try
            {
                // Load JSON data
                string jsonContent = File.ReadAllText(jsonPath);
                List<JsonBookmark> jsonBookmarks = JsonSerializer.Deserialize<List<JsonBookmark>>(jsonContent);

                // Initialize the bookmark editor and bind the source PDF
                using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
                {
                    editor.BindPdf(inputPdfPath);

                    // Convert each top‑level JSON bookmark and add it to the PDF
                    if (jsonBookmarks != null)
                    {
                        foreach (JsonBookmark jb in jsonBookmarks)
                        {
                            Bookmark aspBookmark = ConvertToAsposeBookmark(jb);
                            editor.CreateBookmarks(aspBookmark);
                        }
                    }

                    // Save the modified PDF
                    editor.Save(outputPdfPath);
                }

                Console.WriteLine($"Bookmarks imported successfully. Output saved to '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}