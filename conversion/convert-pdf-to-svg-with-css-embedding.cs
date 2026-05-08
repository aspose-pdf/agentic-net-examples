using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the target SVG file
        const string inputPdfPath  = "input.pdf";
        const string outputSvgPath = "output.svg";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Initialize SVG save options.
            // The current Aspose.Pdf API does not expose a dedicated
            // property for CSS style embedding in SVG; the default
            // behavior embeds necessary style information directly
            // into the generated SVG content.
            Aspose.Pdf.SvgSaveOptions svgOptions = new Aspose.Pdf.SvgSaveOptions();

            // Example of an optional setting – scale the output to pixels.
            // svgOptions.ScaleToPixels = true;

            // Save the PDF as an SVG file using the configured options.
            pdfDocument.Save(outputSvgPath, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG: {outputSvgPath}");
    }
}