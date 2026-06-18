using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputSvgPath = "page1_graphics.svg";
        const int pageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate the requested page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Quick check: does the page contain any vector graphics?
            if (!page.HasVectorGraphics())
            {
                Console.WriteLine("No vector graphics found on the selected page.");
                return;
            }

            // Absorb all graphic elements from the page
            GraphicsAbsorber graphicsAbsorber = new GraphicsAbsorber();
            graphicsAbsorber.Visit(page);

            // Convert the absorbed graphics to an SVG string.
            // The predicate 'g => true' selects all graphic elements.
            SvgExtractor extractor = new SvgExtractor();
            string svgContent = extractor.Extract(graphicsAbsorber, g => true, page);

            // Persist the SVG for further manipulation
            File.WriteAllText(outputSvgPath, svgContent);
            Console.WriteLine($"Vector graphics extracted to '{outputSvgPath}'.");
        }
    }
}