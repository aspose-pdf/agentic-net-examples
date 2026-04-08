using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    // Simple brand palette mapping: replace black (#000000) with brand red (#D32F2F)
    private const string OldColorHex = "#000000";
    private const string NewColorHex = "#D32F2F";

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir    = "ExtractedSvg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Extract vector graphics as SVG strings
                SvgExtractor extractor = new SvgExtractor();
                List<string> svgContents = extractor.Extract(page);

                // If the page contains no vector graphics, continue to next page
                if (svgContents == null || svgContents.Count == 0)
                    continue;

                // Process each extracted SVG
                for (int svgIndex = 0; svgIndex < svgContents.Count; svgIndex++)
                {
                    string svg = svgContents[svgIndex];

                    // Apply brand color transformation (replace old hex color with new one)
                    // This simple replace works for both stroke and fill attributes.
                    svg = svg.Replace(OldColorHex, NewColorHex, StringComparison.OrdinalIgnoreCase);

                    // Build output file name: page_{page}_graphic_{index}.svg
                    string outputFile = Path.Combine(
                        outputDir,
                        $"page_{pageIndex}_graphic_{svgIndex + 1}.svg");

                    // Save the transformed SVG to disk
                    File.WriteAllText(outputFile, svg);
                }
            }
        }

        Console.WriteLine($"Vector graphics extracted and color‑transformed SVGs saved to '{outputDir}'.");
    }
}