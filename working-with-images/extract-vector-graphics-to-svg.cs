using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputSvgPath = "extracted.svg";
        const int pageNumber = 1; // 1‑based index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Check whether the page actually contains vector graphics
            if (!page.HasVectorGraphics())
            {
                Console.WriteLine($"Page {pageNumber} does not contain vector graphics.");
                return;
            }

            // Create a GraphicsAbsorber and collect graphic elements from the page
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(page); // populates the absorber with graphic elements

            // Use SvgExtractor to convert the absorbed graphics to SVG.
            // The predicate 'g => true' accepts all graphic elements.
            SvgExtractor svgExtractor = new SvgExtractor();
            string svgContent = svgExtractor.Extract(absorber, g => true, page);

            // Save the SVG string for further manipulation or inspection
            File.WriteAllText(outputSvgPath, svgContent);
            Console.WriteLine($"Extracted SVG saved to '{outputSvgPath}'.");
        }
    }
}