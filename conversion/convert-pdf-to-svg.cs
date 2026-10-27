using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
// SaveOptions subclasses (including SvgSaveOptions) are also in Aspose.Pdf

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDirPath = "SvgOutput";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirPath);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure SVG save options (default options are sufficient)
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // The base file name for the SVG output.
            // When the PDF has multiple pages Aspose.Pdf will generate
            // output.svg, output_1.svg, output_2.svg, ... automatically.
            string svgBasePath = Path.Combine(outputDirPath, "output.svg");

            // Save the PDF as SVG using the explicit SvgSaveOptions.
            pdfDoc.Save(svgBasePath, svgOptions);
        }

        Console.WriteLine($"PDF has been converted to SVG files in '{outputDirPath}'.");
    }
}