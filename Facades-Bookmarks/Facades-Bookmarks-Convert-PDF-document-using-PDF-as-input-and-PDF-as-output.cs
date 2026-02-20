using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

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
                // Load the source PDF document
                bookmarkEditor.BindPdf(inputPath);

                // Example operation: create bookmarks for all pages
                // (optional – can be omitted if no bookmark changes are needed)
                bookmarkEditor.CreateBookmarks();

                // Save the resulting PDF to the specified output file
                bookmarkEditor.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}