using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Facades;      // For DocumentDevice (if needed)

// Convert a PDF file to SVG format and embed CSS styles (handled internally by the SVG exporter)
class PdfToSvgConverter
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdfPath  = "input.pdf";
        const string outputSvgPath = "output.svg";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize SVG save options
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Enable glyph caching to improve conversion speed (optional)
            svgOptions.CacheGlyphs = true;

            // The SVG exporter embeds necessary CSS styles automatically.
            // No explicit property is required; the exporter handles style embedding.

            // Save the document as SVG
            pdfDocument.Save(outputSvgPath, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG: '{outputSvgPath}'");
    }
}