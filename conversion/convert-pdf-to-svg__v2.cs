using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // DocumentDevice resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputSvgPath = "output.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (creation + loading rule)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize SVG save options (save rule for non‑PDF formats)
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Example setting – cache glyphs to improve conversion performance.
            // No direct CSS embedding option exists for SVG; the SVG output
            // already contains style information required for rendering.
            svgOptions.CacheGlyphs = true;

            // Save the document as SVG using the specified options.
            pdfDocument.Save(outputSvgPath, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG: {outputSvgPath}");
    }
}