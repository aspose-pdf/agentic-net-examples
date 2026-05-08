using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedSvg";
        const string brandColor = "#FF5733"; // Example brand color (hex)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Extract all vector graphics on the page as SVG strings
                SvgExtractor extractor = new SvgExtractor();
                var svgList = extractor.Extract(page); // Returns List<string>

                // Process each extracted SVG
                for (int idx = 0; idx < svgList.Count; idx++)
                {
                    string svgContent = svgList[idx];

                    // Apply brand color transformation (replace fill and stroke colors)
                    svgContent = TransformColors(svgContent, brandColor);

                    // Save the transformed SVG to a file
                    string fileName = Path.Combine(outputDir, $"page_{i}_graphic_{idx + 1}.svg");
                    File.WriteAllText(fileName, svgContent);
                }
            }
        }

        Console.WriteLine("Vector graphics extracted and color‑transformed successfully.");
    }

    // Replaces any fill or stroke color definitions in the SVG with the specified brand color.
    static string TransformColors(string svg, string brandColor)
    {
        // Replace fill attributes (e.g., fill="#000000")
        svg = Regex.Replace(
            svg,
            @"fill\s*=\s*[""']#?[0-9a-fA-F]{3,6}[""']",
            $"fill=\"{brandColor}\"",
            RegexOptions.IgnoreCase);

        // Replace stroke attributes (e.g., stroke="#111111")
        svg = Regex.Replace(
            svg,
            @"stroke\s*=\s*[""']#?[0-9a-fA-F]{3,6}[""']",
            $"stroke=\"{brandColor}\"",
            RegexOptions.IgnoreCase);

        return svg;
    }
}