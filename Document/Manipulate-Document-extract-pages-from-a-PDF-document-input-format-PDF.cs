using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedPages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load source PDF inside a using block for deterministic disposal
            using (Document srcDoc = new Document(inputPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                int pageCount = srcDoc.Pages.Count;

                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a new document for each extracted page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the i‑th page from the source document
                        singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build output file path
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF
                        singlePageDoc.Save(outPath);
                        Console.WriteLine($"Saved page {i} → '{outPath}'.");
                    }
                }
            }

            Console.WriteLine("Page extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}