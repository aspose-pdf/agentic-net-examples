using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_bookmark.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable, so wrap it in a using block
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF file into the editor
            editor.BindPdf(inputPath);

            // Add a bookmark titled "Project Overview" that points to page 5 (1‑based indexing)
            editor.CreateBookmarkOfPage("Project Overview", 5);

            // Persist the changes to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}