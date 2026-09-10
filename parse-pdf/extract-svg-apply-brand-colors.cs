using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir    = "ExtractedSvg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Extract vector graphics as SVG strings using SvgExtractor
                SvgExtractor extractor = new SvgExtractor();
                List<string> svgContents = extractor.Extract(page);

                // Process each extracted SVG
                for (int svgIndex = 0; svgIndex < svgContents.Count; svgIndex++)
                {
                    string originalSvg = svgContents[svgIndex];

                    // ---- Brand colour transformation ----
                    // Example: replace black fill/stroke colours with a brand colour.
                    // Adjust the search/replace patterns to match the actual SVG colour definitions.
                    string transformedSvg = originalSvg
                        .Replace("#000000", "#FF5733")   // black → brand orange
                        .Replace("rgb(0,0,0)", "rgb(255,87,51)"); // alternative format

                    // Determine a unique file name for the transformed SVG
                    string svgFileName = $"page_{pageIndex}_graphic_{svgIndex + 1}.svg";
                    string svgFilePath = Path.Combine(outputDir, svgFileName);

                    // Save the transformed SVG to disk
                    File.WriteAllText(svgFilePath, transformedSvg);
                }
            }
        }

        Console.WriteLine($"Vector graphics extracted and colour‑transformed SVGs saved to '{outputDir}'.");
    }
}