using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Page number to extract vector graphics from (1‑based indexing)
        const int pageNumber = 1;

        // Directory where extracted SVG files will be saved
        const string outputDir = "ExtractedSvgs";

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the target page (Aspose.Pdf uses 1‑based page indexing)
            Page page = doc.Pages[pageNumber];

            // Create an SvgExtractor instance (default options)
            SvgExtractor extractor = new SvgExtractor();

            // Extract all vector graphics on the page to separate SVG files in the output directory
            // This method creates one SVG file per vector graphic element.
            extractor.Extract(page, outputDir);
        }

        Console.WriteLine($"Vector graphics from page {pageNumber} have been extracted to '{outputDir}'.");
    }
}