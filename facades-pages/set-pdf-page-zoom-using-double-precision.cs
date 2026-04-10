using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "zoomed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired zoom factor (e.g., 2.5 = 250%) – use double for high‑resolution precision
        double zoomFactor = 2.5;

        // Apply zoom with PdfPageEditor. The Zoom property expects a float, so cast from double.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.Zoom = (float)zoomFactor; // cast preserves as much precision as the API allows
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom applied ({zoomFactor * 100}%); saved to '{outputPath}'.");
    }
}
