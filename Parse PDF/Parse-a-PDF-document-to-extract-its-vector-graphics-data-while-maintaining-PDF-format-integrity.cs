using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "VectorGraphics";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Skip pages that do not contain vector graphics
                if (!page.HasVectorGraphics())
                    continue;

                // Extract vector graphics from the page as SVG strings
                SvgExtractor extractor = new SvgExtractor();
                var svgList = extractor.Extract(page); // List<string> of SVG content

                // Save each SVG string to a separate file
                int svgIndex = 1;
                foreach (string svgContent in svgList)
                {
                    string fileName = $"page_{pageIndex}_vector_{svgIndex}.svg";
                    string filePath = Path.Combine(outputDir, fileName);
                    File.WriteAllText(filePath, svgContent);
                    svgIndex++;
                }

                // Optional: save all vectors of the page into a single SVG file
                // string singleSvgPath = Path.Combine(outputDir, $"page_{pageIndex}_all_vectors.svg");
                // page.TrySaveVectorGraphics(singleSvgPath);
            }
        }

        Console.WriteLine("Vector graphics extraction completed.");
    }
}