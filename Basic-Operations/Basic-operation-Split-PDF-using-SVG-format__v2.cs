using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions (including SvgSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitSvg";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document srcDoc = new Document(inputPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Create a new temporary document containing only the current page
                    using (Document singlePageDoc = new Document())
                    {
                        singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build the output SVG file path
                        string outPath = Path.Combine(outputDir, $"page_{i}.svg");

                        // Save as SVG – must pass SvgSaveOptions explicitly
                        SvgSaveOptions svgOptions = new SvgSaveOptions();
                        singlePageDoc.Save(outPath, svgOptions);

                        Console.WriteLine($"Saved page {i} as SVG → {outPath}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}