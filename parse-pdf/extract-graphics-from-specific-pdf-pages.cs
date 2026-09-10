using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;
using Aspose.Pdf.Drawing; // SvgExtractor resides in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";
        // Directory where extracted SVG files will be saved
        const string outputDir = "ExtractedGraphics";
        // Page numbers (1‑based) to process
        int[] pagesToExtract = { 1, 3, 5 };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;

            foreach (int pageNum in pagesToExtract)
            {
                // Validate page number (pages are 1‑based)
                if (pageNum < 1 || pageNum > pageCount)
                {
                    Console.Error.WriteLine($"Page {pageNum} is out of range (1‑{pageCount}). Skipping.");
                    continue;
                }

                // Get the page instance
                Page page = doc.Pages[pageNum];

                // Create a GraphicsAbsorber and let it visit the page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    // Collect graphics from the page (fixed per API change)
                    absorber.Visit(page);

                    // Prepare an SVG extractor
                    SvgExtractor extractor = new SvgExtractor();

                    // Predicate that accepts all graphic elements
                    Predicate<GraphicElement> filter = g => true;

                    // Build output file path for this page (disambiguated Path reference)
                    string svgFilePath = System.IO.Path.Combine(outputDir, $"Page_{pageNum}_Graphics.svg");

                    // Extract graphics to an SVG file
                    extractor.Extract(absorber, filter, page, svgFilePath);

                    Console.WriteLine($"Graphics from page {pageNum} saved to: {svgFilePath}");
                }
            }
        }
    }
}
