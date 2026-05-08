using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";

        // Directory where extracted SVG files will be saved
        const string outputDir = "ExtractedGraphics";

        // Page numbers to process (1‑based indexing)
        int[] pagesToExtract = { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // SvgExtractor will convert graphic elements to SVG
            SvgExtractor svgExtractor = new SvgExtractor();

            foreach (int pageNumber in pagesToExtract)
            {
                // Validate page number (Aspose.Pdf uses 1‑based indexing)
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.WriteLine($"Skipping invalid page number: {pageNumber}");
                    continue;
                }

                Page page = doc.Pages[pageNumber];

                // GraphicsAbsorber collects all graphic elements on the page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    // Collect graphics from the page – use Visit instead of Page.Accept
                    absorber.Visit(page);

                    // Define a predicate that accepts all graphic elements
                    Predicate<GraphicElement> allGraphics = g => true;

                    // Build output file path for this page
                    string svgPath = Path.Combine(outputDir, $"page_{pageNumber}.svg");

                    // Extract graphics to an SVG file using the predicate filter
                    svgExtractor.Extract(absorber, allGraphics, page, svgPath);

                    Console.WriteLine($"Extracted graphics from page {pageNumber} to '{svgPath}'.");
                }
            }
        }
    }
}
