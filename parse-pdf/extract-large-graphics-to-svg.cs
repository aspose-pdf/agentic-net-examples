using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "LargeGraphics";
        const double minArea = 5000.0; // Minimum area (points²) to keep a graphic

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Absorb all graphic elements on the current page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page);

                    // Filter elements whose bounding box area exceeds the threshold
                    List<GraphicElement> largeElements = absorber.Elements
                        .Where(e => IsLarge(e, minArea))
                        .ToList();

                    if (largeElements.Count == 0)
                        continue; // No qualifying graphics on this page

                    // Create an extractor (default options)
                    SvgExtractor extractor = new SvgExtractor();

                    // Export each qualifying graphic as an individual SVG file
                    for (int idx = 0; idx < largeElements.Count; idx++)
                    {
                        string svgPath = Path.Combine(
                            outputDir,
                            $"page_{i}_graphic_{idx + 1}.svg");

                        // Extract the selected elements to the SVG file
                        extractor.Extract(largeElements, page, svgPath);
                    }
                }
            }
        }

        Console.WriteLine("Extraction of large graphics completed.");
    }

    // Determines whether a graphic element's bounding box area exceeds the given minimum
    static bool IsLarge(GraphicElement element, double minArea)
    {
        // Some elements may not have a bounding rectangle; treat them as non‑large
        Aspose.Pdf.Rectangle bounds = element.Rectangle; // Fixed: use Rectangle instead of non‑existent BoundingBox
        if (bounds == null)
            return false;

        double width = bounds.URX - bounds.LLX;
        double height = bounds.URY - bounds.LLY;
        return (width * height) > minArea;
    }
}
