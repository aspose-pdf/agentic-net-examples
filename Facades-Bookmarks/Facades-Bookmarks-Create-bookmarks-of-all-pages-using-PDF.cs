using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (will contain bookmarks for all pages)
        const string outputPath = "output_with_bookmarks.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfBookmarkEditor facade
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Load the PDF document
                bookmarkEditor.BindPdf(inputPath);

                // Create bookmarks for every page in the document
                bookmarkEditor.CreateBookmarks();

                // Save the modified PDF with the new bookmarks
                bookmarkEditor.Save(outputPath);
            }

            Console.WriteLine($"Bookmarks created successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}