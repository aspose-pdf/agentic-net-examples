using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load source PDF inside a using block for deterministic disposal
            using (Document src = new Document(inputPdf))
            {
                // Pages collection is 1‑based; iterate from 1 to Count inclusive
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new empty document for the single page
                    using (Document single = new Document())
                    {
                        // Add the current page to the new document
                        single.Pages.Add(src.Pages[i]);

                        // Build output file name
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF (Save() is called inside the using block)
                        single.Save(outPath);
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("Splitting complete.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}