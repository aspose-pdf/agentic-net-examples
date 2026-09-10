using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Vector;
using Aspose.Pdf.Drawing;

// Alias to disambiguate System.IO.Path from Aspose.Pdf.Drawing.Path
using IOPath = System.IO.Path;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "FilteredGraphics";
        const double minArea = 5000.0; // minimum area (points^2) to keep

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

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

                    // Filter elements whose bounding rectangle area exceeds the threshold
                    List<GraphicElement> largeElements = new List<GraphicElement>();
                    foreach (GraphicElement element in absorber.Elements)
                    {
                        // Most GraphicElement derived types expose a Rectangle property (bounding box)
                        if (element.Rectangle != null)
                        {
                            double area = element.Rectangle.Width * element.Rectangle.Height;
                            if (area > minArea)
                                largeElements.Add(element);
                        }
                    }

                    // Export each qualifying element as an individual SVG file
                    for (int i = 0; i < largeElements.Count; i++)
                    {
                        string svgPath = IOPath.Combine(
                            outputDir,
                            $"page{pageNum}_elem{i + 1}.svg");

                        SvgExtractor extractor = new SvgExtractor();
                        // Extract expects a collection; we pass a single‑item list
                        extractor.Extract(new List<GraphicElement> { largeElements[i] }, page, svgPath);
                    }
                }
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}
