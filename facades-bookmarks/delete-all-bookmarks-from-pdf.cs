using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the bookmark editor facade
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Delete all bookmarks
                editor.DeleteBookmarks();

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"All bookmarks removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}