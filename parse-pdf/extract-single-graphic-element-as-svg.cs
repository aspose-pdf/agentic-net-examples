using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file containing vector graphics.
        const string inputPdf = "input.pdf";

        // Index of the graphic element to extract (0‑based).
        const int elementIndex = 0;

        // Output SVG file path for the selected graphic element.
        const string outputSvg = "extracted_element.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Here we work with the first page; adjust as needed.
            Page page = doc.Pages[1];

            // SvgExtractor extracts vector graphics from a page.
            SvgExtractor extractor = new SvgExtractor();

            // Extract all vector graphics on the page as SVG strings.
            // The returned list is zero‑based.
            var svgStrings = extractor.Extract(page);

            if (svgStrings == null || svgStrings.Count == 0)
            {
                Console.WriteLine("No vector graphics found on the page.");
                return;
            }

            if (elementIndex < 0 || elementIndex >= svgStrings.Count)
            {
                Console.WriteLine($"Invalid index. Page contains {svgStrings.Count} graphic element(s).");
                return;
            }

            // Get the SVG content for the requested element.
            string selectedSvg = svgStrings[elementIndex];

            // Write the SVG content to the output file.
            File.WriteAllText(outputSvg, selectedSvg);

            Console.WriteLine($"Graphic element #{elementIndex} saved as SVG to '{outputSvg}'.");
        }
    }
}