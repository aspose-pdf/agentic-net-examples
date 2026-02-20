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
            // Create the bookmark editor facade
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Load the PDF document
                bookmarkEditor.BindPdf(inputPath);

                // Delete all bookmarks from the document
                bookmarkEditor.DeleteBookmarks();

                // Save the modified PDF
                bookmarkEditor.Save(outputPath);
            }

            Console.WriteLine($"All bookmarks have been removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}