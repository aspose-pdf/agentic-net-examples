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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to set the zoom factor for all pages.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            // When ProcessPages is not specified the editor operates on every page.
            editor.Zoom = 1.0f; // 100 % scaling (default)
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with Zoom=1.0 for all pages to '{outputPath}'.");
    }
}
