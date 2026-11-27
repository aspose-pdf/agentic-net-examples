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

        // PdfBookmarkEditor is the correct facade for bookmark operations.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load (bind) the existing PDF document.
            editor.BindPdf(inputPath);

            // Create a top‑level bookmark titled "Editor Note" that points to page 9.
            editor.CreateBookmarkOfPage("Editor Note", 9);

            // Save the modified PDF to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
