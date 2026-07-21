using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    // Simple color transformation: replace any fill or stroke color with the brand color.
    // This example replaces black (#000000) with brand red (#FF0000).
    static string TransformColors(string svgContent)
    {
        // Replace hex color codes (case‑insensitive) for black with brand red.
        // You can extend this method to handle more complex transformations.
        return Regex.Replace(svgContent,
                             @"#000000",
                             "#FF0000",
                             RegexOptions.IgnoreCase);
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputFolder  = "ExtractedSvg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Use SvgExtractor to obtain SVG strings for vector graphics on the page.
                SvgExtractor extractor = new SvgExtractor();
                var svgStrings = extractor.Extract(page); // Returns List<string>

                // Save each extracted SVG after applying the color transformation.
                for (int i = 0; i < svgStrings.Count; i++)
                {
                    string originalSvg = svgStrings[i];
                    string transformedSvg = TransformColors(originalSvg);

                    string fileName = $"page_{pageIndex}_graphic_{i + 1}.svg";
                    string outputPath = Path.Combine(outputFolder, fileName);

                    File.WriteAllText(outputPath, transformedSvg);
                    Console.WriteLine($"Saved transformed SVG: {outputPath}");
                }
            }
        }

        Console.WriteLine("Vector graphic extraction and color transformation completed.");
    }
}