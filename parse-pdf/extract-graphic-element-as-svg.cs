using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file containing vector graphics.
        const string inputPdfPath = "input.pdf";

        // Index of the graphic element to extract (0‑based).
        const int elementIndex = 2; // example: third element

        // Output SVG file path.
        const string outputSvgPath = "extracted_element.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the requested page exists (using first page as example).
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            // Choose the page from which to extract the graphic element.
            Page page = pdfDoc.Pages[1]; // 1‑based indexing

            // SvgExtractor extracts all vector graphics on the page as SVG strings.
            SvgExtractor extractor = new SvgExtractor();

            // Get the list of SVG strings; each entry corresponds to one graphic element.
            var svgStrings = extractor.Extract(page);

            // Validate the requested index.
            if (elementIndex < 0 || elementIndex >= svgStrings.Count)
            {
                Console.Error.WriteLine($"Invalid element index. Page contains {svgStrings.Count} graphic elements.");
                return;
            }

            // Retrieve the SVG content for the specified element.
            string selectedSvg = svgStrings[elementIndex];

            // Save the SVG string to a file.
            File.WriteAllText(outputSvgPath, selectedSvg);

            Console.WriteLine($"Graphic element #{elementIndex} saved as SVG to '{outputSvgPath}'.");
        }
    }
}