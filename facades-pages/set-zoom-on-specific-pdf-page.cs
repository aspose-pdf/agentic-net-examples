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

        // Use PdfPageEditor to apply zoom to specific pages.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = new int[] { 7 }; // target page (1‑based index)
            editor.Zoom = 2.0f; // 200 % magnification
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 7 zoom set to 2.0 and saved to '{outputPath}'.");
    }
}
