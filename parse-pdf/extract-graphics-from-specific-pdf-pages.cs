using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPdf = "input.pdf";

        // Output directory for extracted SVG graphics
        const string outputDir = "ExtractedGraphics";

        // Page numbers to process (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToExtract = { 1, 3, 5 };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block (document‑disposal‑with‑using rule)
        using (Document doc = new Document(inputPdf))
        {
            // Validate requested page numbers
            foreach (int pageNum in pagesToExtract)
            {
                if (pageNum < 1 || pageNum > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page number {pageNum} is out of range. Skipping.");
                    continue;
                }

                // Get the page (1‑based indexing rule)
                Page page = doc.Pages[pageNum];

                // Create a GraphicsAbsorber to collect graphic elements on this page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    // Perform the search on the page
                    absorber.Visit(page);

                    // If no graphics were found, continue to next page
                    if (absorber.Elements.Count == 0)
                    {
                        Console.WriteLine($"No graphics found on page {pageNum}.");
                        continue;
                    }

                    // Prepare a filter that accepts all graphic elements
                    Predicate<GraphicElement> allGraphicsFilter = ge => true;

                    // Use SvgExtractor to write each graphic to a separate SVG file
                    SvgExtractor extractor = new SvgExtractor();

                    int graphicIndex = 1;
                    foreach (GraphicElement element in absorber.Elements)
                    {
                        // Build a unique file name for each graphic
                        string svgPath = Path.Combine(
                            outputDir,
                            $"page_{pageNum}_graphic_{graphicIndex}.svg");

                        // Extract the current graphic element to the SVG file
                        // (Extract(GraphicsAbsorber, Predicate<GraphicElement>, Page, string) overload)
                        extractor.Extract(absorber, allGraphicsFilter, page, svgPath);

                        Console.WriteLine($"Extracted graphic {graphicIndex} from page {pageNum} to '{svgPath}'.");
                        graphicIndex++;
                    }
                }
            }
        }

        Console.WriteLine("Graphics extraction completed.");
    }
}