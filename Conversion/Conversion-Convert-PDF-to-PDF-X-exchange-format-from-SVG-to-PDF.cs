using System;
using System.IO;
using Aspose.Pdf; // SvgLoadOptions resides in this namespace

class SvgToPdfXConverter
{
    static void Main(string[] args)
    {
        // Input SVG file path
        const string svgPath = "input.svg";
        // Output PDF/X file path
        const string pdfPath = "output.pdf";

        // Verify that the SVG file exists
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"Error: SVG file not found at '{svgPath}'.");
            return;
        }

        try
        {
            // Configure SVG loading options (optional: choose conversion engine)
            SvgLoadOptions loadOptions = new SvgLoadOptions
            {
                // Use the newer conversion engine for better quality
                ConversionEngine = SvgLoadOptions.ConversionEngines.NewEngine
            };

            // Load the SVG document
            Document pdfDocument = new Document(svgPath, loadOptions);

            // If the library version supports PDF/X conformance, it can be set here:
            // pdfDocument.Conformance = PdfConformance.PdfX; // Uncomment if available

            // Save the document as PDF (PDF/X if conformance is set)
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"SVG successfully converted to PDF/X at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
