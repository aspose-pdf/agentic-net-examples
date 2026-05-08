using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputSvg = "page1_graphics.svg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using block for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (global rule)
            Page page = doc.Pages[1];

            // Verify that the page actually contains vector graphics
            if (!page.HasVectorGraphics())
            {
                Console.WriteLine("The selected page does not contain vector graphics.");
                return;
            }

            // Create a GraphicsAbsorber and collect graphics from the page
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(page);

            // Create an SvgExtractor (default options)
            SvgExtractor extractor = new SvgExtractor();

            // Predicate that selects all graphic elements
            Predicate<GraphicElement> allGraphics = g => true;

            // Extract the collected graphics to an SVG file
            extractor.Extract(absorber, allGraphics, page, outputSvg);

            Console.WriteLine($"Vector graphics extracted to '{outputSvg}'.");
        }
    }
}