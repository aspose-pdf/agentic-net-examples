using System;
using System.IO;
using Aspose.Pdf;               // Document, SvgSaveOptions
using Aspose.Pdf.Facades;      // (optional, not required for this task)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Base output SVG file name.
        // Aspose.Pdf will create separate SVG files for each page:
        // output.svg, output_2.svg, output_3.svg, ...
        const string outputSvgBase = "output.svg";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure SVG save options
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Ensure each page is saved as an individual SVG file.
            // When TreatTargetFileNameAsDirectory is false (default),
            // the first page is saved as the name provided,
            // subsequent pages get a numeric suffix.
            svgOptions.TreatTargetFileNameAsDirectory = false;

            // Optional: enable multi‑threaded processing for faster conversion
            // svgOptions.IsMultiThreading = true;

            // Save the PDF as SVG files using the configured options
            pdfDocument.Save(outputSvgBase, svgOptions);
        }

        Console.WriteLine("PDF has been split into separate SVG files.");
    }
}