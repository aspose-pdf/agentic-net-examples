using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    // Minimum area (in points^2) a graphic must have to be exported.
    const double MinArea = 5000.0; // adjust as needed

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExportedGraphics";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Absorb all vector graphics on the current page.
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page); // collect graphics

                    // Prepare an SVG extractor (default options).
                    SvgExtractor extractor = new SvgExtractor();

                    int graphicCounter = 0;

                    // Examine each captured graphic element.
                    foreach (GraphicElement element in absorber.Elements)
                    {
                        // Compute the bounding rectangle area.
                        Aspose.Pdf.Rectangle rect = element.Rectangle;
                        double width  = rect.URX - rect.LLX;
                        double height = rect.URY - rect.LLY;
                        double area   = width * height;

                        // Export only if the area exceeds the defined threshold.
                        if (area > MinArea)
                        {
                            graphicCounter++;
                            string svgPath = Path.Combine(
                                outputDir,
                                $"{Path.GetFileNameWithoutExtension(inputPdf)}_page{pageIndex}_g{graphicCounter}.svg");

                            // Export the single graphic element to an SVG file.
                            // The overload that accepts an IEnumerable<GraphicElement> is used.
                            extractor.Extract(new List<GraphicElement> { element }, page, svgPath);
                        }
                    }
                }
            }
        }

        Console.WriteLine("Graphic extraction completed.");
    }
}