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

        // Temporary file to hold the rotated PDF
        string tempRotatedPath = Path.GetTempFileName();

        // Rotate all pages 90 degrees using PdfPageEditor (facade API)
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPath);          // Load source PDF
            pageEditor.Rotation = 90;               // Rotate each page by 90°
            pageEditor.ApplyChanges();              // Apply the rotation
            pageEditor.Save(tempRotatedPath);       // Save intermediate result
        }

        // Add uniform margins (10 points on each side) using PdfFileEditor
        // PdfFileEditor does NOT implement IDisposable, so we instantiate it without a using block.
        var fileEditor = new PdfFileEditor();
        try
        {
            int[] pages = null;                     // null => apply to all pages
            double left   = 10;
            double right  = 10;
            double top    = 10;
            double bottom = 10;

            // Input file, output file, pages array, left, right, top, bottom
            fileEditor.AddMargins(tempRotatedPath, outputPath, pages, left, right, top, bottom);
        }
        finally
        {
            // If future versions implement IDisposable, we could call Dispose here.
            // Currently no disposal is required.
        }

        // Clean up the temporary file
        try { File.Delete(tempRotatedPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
