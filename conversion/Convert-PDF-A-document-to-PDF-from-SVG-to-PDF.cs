using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input SVG file (generated from a PDF/A document or any SVG source)
        const string svgInputPath  = "input.svg";
        // Desired output PDF file (standard PDF, not PDF/A)
        const string pdfOutputPath = "output.pdf";

        // Verify that the SVG file exists before attempting conversion
        if (!File.Exists(svgInputPath))
        {
            Console.Error.WriteLine($"Error: SVG file not found – {svgInputPath}");
            return;
        }

        // Configure SVG loading options.
        // The ConversionEngine property selects which engine to use for SVG → PDF conversion.
        // NewEngine provides the latest conversion algorithm.
        SvgLoadOptions loadOptions = new SvgLoadOptions
        {
            ConversionEngine = SvgLoadOptions.ConversionEngines.NewEngine
        };

        // Load the SVG document using the specified options.
        // The Document constructor with a file name and LoadOptions handles the loading.
        using (Document svgDoc = new Document(svgInputPath, loadOptions))
        {
            // Save the loaded document as a regular PDF.
            // No additional SaveOptions are required for PDF output.
            svgDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"SVG successfully converted to PDF: '{pdfOutputPath}'");
    }
}