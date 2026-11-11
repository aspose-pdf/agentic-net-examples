using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to set zoom for specific pages.
        // ProcessPages expects 1‑based page numbers.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = new int[] { 3 };   // page three
            editor.Zoom = 1.5f;                     // 150 % magnification
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 3 zoom set to 150 % and saved as '{outputPath}'.");
    }
}