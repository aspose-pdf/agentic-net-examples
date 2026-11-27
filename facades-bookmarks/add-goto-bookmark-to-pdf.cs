using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Use PdfBookmarkEditor within a using block to ensure resources are released
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Bind the existing PDF document
                bookmarkEditor.BindPdf(inputPath);

                // Create a new bookmark that goes to page 9 (1‑based page numbers)
                Bookmark bm = new Bookmark
                {
                    Title = "Go to Page 9",
                    Action = "GoTo",
                    PageNumber = 9 // correct property name for the facade Bookmark
                };

                // Add the bookmark to the PDF – the facade method is CreateBookmarks(Bookmark)
                bookmarkEditor.CreateBookmarks(bm);

                // Save the modified PDF
                bookmarkEditor.Save(outputPath);
            }

            Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
