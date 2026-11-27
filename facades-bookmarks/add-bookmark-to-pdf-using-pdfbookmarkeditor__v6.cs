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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor provides bookmark manipulation in the Facades API.
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Load (bind) the existing PDF document
                editor.BindPdf(inputPath);

                // Create a bookmark titled "Editor Bookmark" on page 7 (pages are 1‑based)
                editor.CreateBookmarkOfPage("Editor Bookmark", 7);

                // Save the modified PDF
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
