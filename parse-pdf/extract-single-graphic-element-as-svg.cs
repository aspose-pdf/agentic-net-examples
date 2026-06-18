using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file containing vector graphics
        const string inputPdfPath = "input.pdf";

        // Output SVG file for the selected graphic element
        const string outputSvgPath = "element.svg";

        // Zero‑based index of the graphic element to extract
        const int elementIndex = 0;

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Select the page from which to extract the graphic element (first page here)
            Page page = pdfDoc.Pages[1]; // Aspose.Pdf uses 1‑based indexing for pages

            // Create an SvgExtractor instance (default options)
            SvgExtractor extractor = new SvgExtractor();

            // Extract all vector graphics on the page as SVG strings
            var svgStrings = extractor.Extract(page);

            // Validate the requested index
            if (elementIndex < 0 || elementIndex >= svgStrings.Count)
            {
                Console.Error.WriteLine($"Invalid element index. Page contains {svgStrings.Count} graphic element(s).");
                return;
            }

            // Retrieve the SVG content for the specified element
            string selectedSvg = svgStrings[elementIndex];

            // Write the SVG content to the output file
            File.WriteAllText(outputSvgPath, selectedSvg);

            Console.WriteLine($"Graphic element #{elementIndex} extracted and saved to '{outputSvgPath}'.");
        }
    }
}