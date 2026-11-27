using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor provides bookmark manipulation in the Facades API.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF.
            editor.BindPdf(inputPath);

            // Create a bookmark titled "Editor Section" that points to page 12 (1‑based).
            editor.CreateBookmarkOfPage("Editor Section", 12);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
