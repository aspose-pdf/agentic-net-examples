using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF to split
        const string outputDir = "SplitPages";        // folder for individual pages

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF inside a using block (deterministic disposal)
            using (Document src = new Document(inputPdf))
            {
                // Aspose.Pdf uses 1‑based page indexing – iterate from 1 to Count inclusive
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new empty PDF for the single page
                    using (Document single = new Document())
                    {
                        // Add the i‑th page from the source document
                        single.Pages.Add(src.Pages[i]);

                        // Build output file name, e.g. "Page_1.pdf"
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF
                        single.Save(outPath);
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