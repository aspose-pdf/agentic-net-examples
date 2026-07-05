using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedSvgs";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Specify the page number (1‑based indexing)
                int pageNumber = 1;
                if (pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNumber} does not exist.");
                    return;
                }

                Page page = doc.Pages[pageNumber];

                // Verify that the page contains vector graphics
                if (!page.HasVectorGraphics())
                {
                    Console.WriteLine("No vector graphics on the specified page.");
                    return;
                }

                // Create a GraphicsAbsorber and collect graphic elements from the page
                GraphicsAbsorber absorber = new GraphicsAbsorber();
                absorber.Visit(page);

                // Use SvgExtractor to convert the absorbed graphics to SVG
                SvgExtractor extractor = new SvgExtractor();

                // Simple filter that accepts all graphic elements
                Predicate<GraphicElement> filter = ge => true;

                // Extract SVG content as a single string
                string svgContent = extractor.Extract(absorber, filter, page);

                // Save the SVG string to a file for further manipulation
                string svgPath = Path.Combine(outputDir, $"page{pageNumber}.svg");
                File.WriteAllText(svgPath, svgContent);
                Console.WriteLine($"Extracted SVG saved to {svgPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}