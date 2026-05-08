using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "zoomed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired zoom factor (double precision). 1.0 = 100%
        double zoomFactor = 2.5; // 250% scaling for high‑resolution output

        // PdfPageEditor is a facade that edits page content (zoom, rotation, etc.)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Set zoom. Property type is float, so cast from double.
            editor.Zoom = (float)zoomFactor;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom applied ({zoomFactor * 100}%); saved to '{outputPath}'.");
    }
}