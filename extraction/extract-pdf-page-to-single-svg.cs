using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF
        const string outputSvgPath = "page24.svg"; // destination SVG
        const int    pageNumber    = 24;           // page to extract (1‑based)

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document using the core API
        var doc = new Document(inputPdfPath);

        // Validate the requested page number
        if (pageNumber < 1 || pageNumber > doc.Pages.Count)
        {
            Console.Error.WriteLine($"Page {pageNumber} is out of range. Document contains {doc.Pages.Count} pages.");
            return;
        }

        // Configure SVG extraction – do NOT create a separate SVG for each sub‑path
        var options = new SvgExtractionOptions
        {
            ExtractEverySubPathToSvg = false
        };

        // Initialise the extractor with the options (SvgExtractor lives in Aspose.Pdf.Vector)
        var extractor = new SvgExtractor(options);

        // Extract the specified page to a single SVG file
        extractor.Extract(doc.Pages[pageNumber], outputSvgPath);

        Console.WriteLine($"Page {pageNumber} extracted to SVG: {outputSvgPath}");
    }
}
