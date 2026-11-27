using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "updated.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor is the correct facade for bookmark manipulation.
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Bind the existing PDF document.
                editor.BindPdf(sourcePath);

                // Create a bookmark that points to page 1 (1‑based index).
                editor.CreateBookmarkOfPage("My Bookmark", 1);

                // Save the modified PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
