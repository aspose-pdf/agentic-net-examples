using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "zoomed_output.pdf";

        // Desired zoom factor as double for high‑resolution precision
        double zoomFactor = 1.25; // 125 % scaling

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfPageEditor (a Facade) to modify the PDF page zoom
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // Set the zoom coefficient. The property type is float,
            // so cast the double value explicitly to preserve precision.
            editor.Zoom = (float)zoomFactor;

            // Save the modified PDF to the output path
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Zoom applied (factor {zoomFactor}) and saved to '{outputPdf}'.");
    }
}