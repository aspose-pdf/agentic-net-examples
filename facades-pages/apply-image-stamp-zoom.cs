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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Non‑consecutive pages to which the zoom will be applied
        int[] pagesToZoom = new int[] { 1, 3, 5 };

        // Apply a common zoom factor using PdfPageEditor
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = pagesToZoom;
            editor.Zoom = 0.8f;
            editor.Save(outputPath);
        }

        Console.WriteLine("Zoom applied to pages 1, 3, 5 and saved as " + outputPath);
    }
}