using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    // Minimum area (in points^2) a graphic must have to be exported
    const double MinArea = 1000.0;

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExportedGraphics";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output root folder exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Absorb all graphic elements on the current page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page);

                    // Predicate that checks whether a graphic's bounding rectangle
                    // exceeds the defined minimum area
                    Predicate<GraphicElement> areaFilter = g =>
                    {
                        var rect = g.Rectangle; // Bounding rectangle of the graphic
                        double width  = rect.URX - rect.LLX;
                        double height = rect.URY - rect.LLY;
                        double area   = width * height;
                        return area > MinArea;
                    };

                    // Prepare a folder for this page's exported graphics
                    string pageFolder = Path.Combine(outputDir, $"Page_{pageNum}");
                    Directory.CreateDirectory(pageFolder);

                    // Export matching graphics to separate SVG files
                    // The SvgExtractor overload with a predicate extracts all matching
                    // elements into a single SVG file; to get one file per element we
                    // iterate manually.
                    var matchingElements = absorber.Elements
                                                   .Where(g => areaFilter(g))
                                                   .ToList();

                    int graphicIndex = 1;
                    foreach (var element in matchingElements)
                    {
                        string svgPath = Path.Combine(pageFolder, $"Graphic_{graphicIndex}.svg");
                        // Extract the single element to an SVG file
                        SvgExtractor extractor = new SvgExtractor();
                        extractor.Extract(new List<GraphicElement> { element }, page, svgPath);
                        graphicIndex++;
                    }
                }
            }
        }

        Console.WriteLine("Export completed.");
    }
}