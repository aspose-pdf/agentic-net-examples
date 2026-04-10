using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output SVG file that will contain the extracted vector graphics
        const string outputSvgPath = "extracted_page1.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Choose the page to process (1‑based indexing)
            int pageNumber = 1;
            if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            Page page = pdfDoc.Pages[pageNumber];

            // Create a GraphicsAbsorber which will collect all vector graphic elements on the page
            using (GraphicsAbsorber graphicsAbsorber = new GraphicsAbsorber())
            {
                // Perform the search on the selected page
                graphicsAbsorber.Visit(page);

                // If no vector graphics are found, inform the user
                if (graphicsAbsorber.Elements.Count == 0)
                {
                    Console.WriteLine("No vector graphics found on the selected page.");
                    return;
                }

                // Prepare a predicate that accepts every graphic element
                Predicate<GraphicElement> acceptAll = element => true;

                // Use SvgExtractor to convert the collected graphics to an SVG string
                SvgExtractor svgExtractor = new SvgExtractor();
                string svgContent = svgExtractor.Extract(graphicsAbsorber, acceptAll, page);

                // Write the SVG content to a file for further manipulation
                File.WriteAllText(outputSvgPath, svgContent);
                Console.WriteLine($"Vector graphics extracted to SVG file: {outputSvgPath}");
            }
        }
    }
}