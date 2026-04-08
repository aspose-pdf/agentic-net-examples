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
        int[] selectedPages = { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // SvgExtractor is used to convert graphic elements to SVG
            SvgExtractor svgExtractor = new SvgExtractor();

            foreach (int pageNumber in selectedPages)
            {
                // Validate page number (Aspose.Pdf uses 1‑based indexing)
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.WriteLine($"Page {pageNumber} is out of range, skipping.");
                    continue;
                }

                // Get the page object
                Page page = doc.Pages[pageNumber];

                // GraphicsAbsorber collects all graphic elements on the page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    // Perform the search on the page
                    absorber.Visit(page);

                    // Extract all collected graphics as a single SVG string.
                    // The predicate 'element => true' means no filtering – all graphics are included.
                    string svgContent = svgExtractor.Extract(absorber, element => true, page);

                    // Save the SVG content to a file named after the page number
                    string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.svg");
                    File.WriteAllText(outputPath, svgContent);

                    Console.WriteLine($"Graphics from page {pageNumber} saved to '{outputPath}'.");
                }
            }
        }
    }
}