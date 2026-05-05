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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Add a bookmark titled "Project Overview" that navigates to page 5
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Create the bookmark (page numbers are 1‑based)
            editor.CreateBookmarkOfPage("Project Overview", 5);

            // Persist the changes
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added. Output saved to '{outputPath}'.");
    }
}