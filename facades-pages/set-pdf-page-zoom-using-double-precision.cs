using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "zoomed_output.pdf";

        // Desired zoom factor (1.0 = 100%). Using double for precision.
        double zoomFactor = 1.75; // 175% scaling

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade and bind the loaded document.
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Set the zoom. The property expects a float, so cast the double value.
            editor.Zoom = (float)zoomFactor;

            // Apply the zoom changes to the document pages.
            editor.ApplyChanges();

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Zoom applied and saved to '{outputPath}'.");
    }
}