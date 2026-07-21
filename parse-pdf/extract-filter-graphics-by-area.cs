using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedSvgs";
        const double minArea = 5000.0; // Minimum area (points²) to keep a graphic

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (Document doc = new Document(inputPdf))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Collect all graphic elements on the page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page);

                    // Predicate that keeps only elements whose bounding rectangle area exceeds minArea
                    Predicate<GraphicElement> filter = element =>
                    {
                        var rect = element.Rectangle;
                        if (rect == null) return false;
                        double width = rect.URX - rect.LLX;
                        double height = rect.URY - rect.LLY;
                        return (width * height) > minArea;
                    };

                    // If any elements satisfy the predicate, extract them to an SVG file
                    if (absorber.Elements.Count > 0)
                    {
                        string outPath = Path.Combine(outputDir, $"page_{i}_filtered.svg");
                        SvgExtractor extractor = new SvgExtractor();
                        extractor.Extract(absorber, filter, page, outPath);
                        Console.WriteLine($"Extracted filtered SVG for page {i} to {outPath}");
                    }
                }
            }
        }
    }
}