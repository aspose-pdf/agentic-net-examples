using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputSvgPath = "output.svg"; // base name for SVG output

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize SVG save options
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Create a separate directory (named after the output file) and save each page as an individual SVG file
                svgOptions.TreatTargetFileNameAsDirectory = true;

                // Save the PDF as SVG using the explicit save options (required for non‑PDF formats)
                pdfDoc.Save(outputSvgPath, svgOptions);
            }

            Console.WriteLine("PDF successfully split into individual SVG files.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}