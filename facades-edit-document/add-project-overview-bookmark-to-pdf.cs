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

        // Initialize the bookmark editor, bind the source PDF,
        // create the bookmark pointing to page 5, and save the result.
        using (Aspose.Pdf.Facades.PdfBookmarkEditor editor = new Aspose.Pdf.Facades.PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);
            editor.CreateBookmarkOfPage("Project Overview", 5);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}