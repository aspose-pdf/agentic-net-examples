using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "GraphicsOutput";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Page numbers to process (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToExtract = { 1, 3, 5 };

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            foreach (int pageNum in pagesToExtract)
            {
                // Validate page number
                if (pageNum < 1 || pageNum > doc.Pages.Count)
                {
                    Console.WriteLine($"Skipping invalid page number {pageNum}");
                    continue;
                }

                Page page = doc.Pages[pageNum];

                // Create a GraphicsAbsorber and visit the page (lifecycle rule: use using)
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page); // Collect graphic elements on the page

                    // Use SvgExtractor to convert the collected graphics to SVG
                    SvgExtractor extractor = new SvgExtractor();

                    // Create a sub‑directory for this page's output
                    string pageDir = Path.Combine(outputDir, $"Page_{pageNum}");
                    Directory.CreateDirectory(pageDir);

                    // Extract all graphics to a single SVG file.
                    // The predicate "_ => true" accepts every graphic element.
                    string svgFilePath = Path.Combine(pageDir, "graphics.svg");
                    extractor.Extract(absorber, _ => true, page, svgFilePath);

                    // If individual SVG strings are needed instead of a file, use:
                    // string svgContent = extractor.Extract(absorber, _ => true, page);
                    // File.WriteAllText(Path.Combine(pageDir, "graphics.svg"), svgContent);
                }
            }
        }

        Console.WriteLine("Graphics extraction completed.");
    }
}