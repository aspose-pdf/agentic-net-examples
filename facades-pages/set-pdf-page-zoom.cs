using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "zoomed_output.pdf";

        // Desired zoom factor (e.g., 2.5 = 250% scaling)
        double zoomFactor = 2.5;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facade) to modify the PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Set zoom; property expects float, so cast the double value
            editor.Zoom = (float)zoomFactor;

            // Apply the changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom applied and saved to '{outputPath}'.");
    }
}