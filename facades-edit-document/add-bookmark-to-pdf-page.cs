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
            // Initialize the bookmark editor and bind the source PDF.
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(inputPath);

            // Create a bookmark titled "Project Overview" that points to page 5.
            editor.CreateBookmarkOfPage("Project Overview", 5);

            // Save the modified PDF with the new bookmark.
            editor.Save(outputPath);
            editor.Close();

            Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}