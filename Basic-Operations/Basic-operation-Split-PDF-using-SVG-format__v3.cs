using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "SplitSvgPages";      // directory for SVG pages

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF
            using (Document srcDoc = new Document(inputPdf))
            {
                // Iterate over all pages (1‑based indexing)
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Create a new document containing only the current page
                    using (Document pageDoc = new Document())
                    {
                        pageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build output file name, e.g. Page_1.svg
                        string outPath = Path.Combine(outputDir, $"Page_{i}.svg");

                        // SVG save options (required for non‑PDF output)
                        SvgSaveOptions svgOpts = new SvgSaveOptions();

                        // Save the single‑page document as SVG
                        pageDoc.Save(outPath, svgOpts);

                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF split to SVG completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}