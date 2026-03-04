using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "SplitPages";         // folder for split pages

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load source document – wrapped in using for deterministic disposal
            using (Document srcDoc = new Document(inputPdf))
            {
                // Aspose.Pdf uses 1‑based page indexing (rule: page-indexing-one-based)
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Create a new empty document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the i‑th page from the source document
                        singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build output file name (e.g., Page_1.pdf)
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page document – PDF format, no SaveOptions needed
                        singlePageDoc.Save(outPath);

                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during splitting: {ex.Message}");
        }
    }
}