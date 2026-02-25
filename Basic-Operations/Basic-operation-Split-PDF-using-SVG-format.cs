using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "SplitSvg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the source PDF inside a using block for deterministic disposal
        using (Document srcDoc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= srcDoc.Pages.Count; i++)
            {
                // Create a new document that will contain only the current page
                using (Document singlePageDoc = new Document())
                {
                    // Add the page from the source document
                    singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                    // Prepare SVG save options (all save options are in Aspose.Pdf namespace)
                    SvgSaveOptions svgOptions = new SvgSaveOptions();

                    // Build the output file name for this page
                    string outPath = Path.Combine(outputDir, $"Page_{i}.svg");

                    // Save the single‑page document as SVG using explicit save options
                    singlePageDoc.Save(outPath, svgOptions);

                    Console.WriteLine($"Saved page {i} as SVG → {outPath}");
                }
            }
        }

        Console.WriteLine("PDF split to SVG completed.");
    }
}