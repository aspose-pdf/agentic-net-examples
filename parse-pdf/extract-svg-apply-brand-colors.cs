using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedSvg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Extract SVG strings from the current page
                SvgExtractor extractor = new SvgExtractor();
                var svgContents = extractor.Extract(page); // List<string>

                // Process each extracted SVG
                for (int i = 0; i < svgContents.Count; i++)
                {
                    string svg = svgContents[i];

                    // Example brand palette transformation:
                    // Replace any black fill or stroke (#000000) with brand red (#FF5733)
                    // Adjust as needed for other colors.
                    svg = Regex.Replace(svg, @"(#000000)", "#FF5733", RegexOptions.IgnoreCase);

                    // Optionally, replace other colors here...

                    // Determine output file name
                    string fileName = $"page_{pageIndex}_graphic_{i + 1}.svg";
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the transformed SVG to disk
                    File.WriteAllText(outputPath, svg);
                }
            }
        }

        Console.WriteLine($"SVG extraction and color transformation completed. Files saved to '{outputFolder}'.");
    }
}