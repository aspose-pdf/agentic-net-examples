using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Create the bookmark editor (does not implement IDisposable)
                PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

                // Bind the loaded document to the editor
                bookmarkEditor.BindPdf(doc);

                // Create individual Bookmark objects for the required pages
                Bookmark bmA = new Bookmark { Title = "Segment A", PageNumber = 4 };
                Bookmark bmB = new Bookmark { Title = "Segment B", PageNumber = 5 };
                Bookmark bmC = new Bookmark { Title = "Segment C", PageNumber = 6 };

                // Add the bookmarks to the document via the editor
                bookmarkEditor.CreateBookmarks(bmA);
                bookmarkEditor.CreateBookmarks(bmB);
                bookmarkEditor.CreateBookmarks(bmC);

                // Save the modified PDF with the new bookmarks
                bookmarkEditor.Save(outputPath);
                // Optional: release resources held by the editor
                bookmarkEditor.Close();
            }

            Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
