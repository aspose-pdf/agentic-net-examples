using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Define non‑consecutive pages (1‑based indexing)
        int[] pagesToZoom = new int[] { 1, 3, 5 };
        // Common zoom factor (1.0 = 100%)
        double zoomFactor = 1.5; // 150%

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfPageEditor implements IDisposable – use using for deterministic disposal
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Specify which pages to edit
            editor.ProcessPages = pagesToZoom;

            // PdfPageEditor.Zoom expects an integer percentage, not a double.
            // Convert the double zoom factor to an integer percentage.
            editor.Zoom = (int)(zoomFactor * 100);

            // Commit the changes
            editor.ApplyChanges();

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"Applied zoom factor {zoomFactor} to pages {string.Join(", ", pagesToZoom)} and saved to '{outputPath}'.");
    }
}
