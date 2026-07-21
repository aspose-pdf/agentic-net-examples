using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Devices;      // For device‑related classes (if needed)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output SVG file path (first page). Additional pages will be saved as
        // input_2.svg, input_3.svg, etc. automatically.
        const string outputSvgPath = "output.svg";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize default SVG save options
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Save the PDF as SVG images using the default options.
            // Because the target format is not PDF, we must pass the SaveOptions
            // explicitly (see the "save-to-non-pdf-always-use-save-options" rule).
            pdfDocument.Save(outputSvgPath, svgOptions);
        }

        Console.WriteLine($"PDF has been converted to SVG images at '{outputSvgPath}'.");
    }
}