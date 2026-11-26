using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions (including SvgSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SvgPages";

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
            using (Document doc = new Document(inputPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Create a temporary document that will contain only the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page from the source document
                        singlePageDoc.Pages.Add(doc.Pages[i]);

                        // Configure SVG save options (required for non‑PDF output)
                        SvgSaveOptions svgOptions = new SvgSaveOptions();

                        // Build the output file name for this page
                        string outPath = Path.Combine(outputDir, $"Page_{i}.svg");

                        // Save the single‑page document as SVG
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