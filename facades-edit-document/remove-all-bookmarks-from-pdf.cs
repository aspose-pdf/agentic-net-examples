using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_bookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor implements IDisposable – use a using block
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Load the PDF
                editor.BindPdf(inputPath);

                // Delete all existing bookmarks (the correct method name)
                editor.DeleteBookmarks();

                // Save the PDF without bookmarks
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
