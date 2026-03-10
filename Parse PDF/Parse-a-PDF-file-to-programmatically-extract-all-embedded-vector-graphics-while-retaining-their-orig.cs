using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector; // SvgExtractor resides here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "VectorGraphics";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle: using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based (rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Skip pages without vector graphics (Page.HasVectorGraphics)
                if (!page.HasVectorGraphics())
                    continue;

                // SvgExtractor extracts vector graphics as SVG strings
                SvgExtractor extractor = new SvgExtractor();

                // Extract returns a List<string> where each entry is an SVG image
                var svgContents = extractor.Extract(page);

                // Save each SVG string to a separate .svg file
                for (int j = 0; j < svgContents.Count; j++)
                {
                    string fileName = $"page_{i}_vector_{j + 1}.svg";
                    string filePath = Path.Combine(outputDir, fileName);
                    File.WriteAllText(filePath, svgContents[j]);
                }
            }
        }

        Console.WriteLine($"All vector graphics have been extracted to '{outputDir}'.");
    }
}