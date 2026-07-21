using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Page number (1‑based) from which to extract the graphic element
        const int pageNumber = 1;
        // Zero‑based index of the graphic element on the page
        const int elementIndex = 0;
        // Output SVG file path
        const string outputSvg = "element.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Validate page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Get the requested page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[pageNumber];

            // Create an SvgExtractor instance
            SvgExtractor extractor = new SvgExtractor();

            // Extract all vector graphics on the page as SVG strings
            var svgStrings = extractor.Extract(page);

            // Validate the requested element index
            if (elementIndex < 0 || elementIndex >= svgStrings.Count)
            {
                Console.Error.WriteLine("Element index out of range.");
                return;
            }

            // Retrieve the SVG content for the specified element
            string svgContent = svgStrings[elementIndex];

            // Save the SVG content to a file
            File.WriteAllText(outputSvg, svgContent);
        }

        Console.WriteLine($"Graphic element saved as SVG to '{outputSvg}'.");
    }
}