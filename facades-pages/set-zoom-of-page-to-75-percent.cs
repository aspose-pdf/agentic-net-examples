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

        // Use PdfPageEditor to modify page‑level settings.
        // The Zoom factor is applied via the Zoom property together with the ProcessPages array
        // which specifies the 1‑based page numbers to affect.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = new int[] { 5 };   // target page 5 (1‑based index)
            editor.Zoom = 0.75f;                     // 75 % view (0.75 factor)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom of page 5 set to 75% and saved to '{outputPath}'.");
    }
}
