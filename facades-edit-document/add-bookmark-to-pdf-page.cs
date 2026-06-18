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

        // Use PdfBookmarkEditor from Aspose.Pdf.Facades to add a bookmark.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Create a bookmark titled "Project Overview" that points to page 5.
            editor.CreateBookmarkOfPage("Project Overview", 5);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}