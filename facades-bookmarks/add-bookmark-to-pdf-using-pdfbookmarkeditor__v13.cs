using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor provides bookmark manipulation in the Facades API
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPath);

            // Create a bookmark titled "Editor Note" on page 8 (pages are 1‑based)
            editor.CreateBookmarkOfPage("Editor Note", 8);

            // Save the modified PDF to the output path
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
