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

        // Apply a Split transition of 2 seconds to page 3
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);                 // load source PDF
            editor.ProcessPages = new int[] { 3 };     // target page (1‑based)
            editor.TransitionType = 5;                 // Split transition (integer value)
            editor.TransitionDuration = 2;            // seconds
            editor.Save(outputPath);                   // write the modified PDF
        }

        Console.WriteLine($"Page 3 transition set to Split (2 s) and saved to '{outputPath}'.");
    }
}