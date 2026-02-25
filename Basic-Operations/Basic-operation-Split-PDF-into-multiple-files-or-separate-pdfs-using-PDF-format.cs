using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "SplitPages";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (wrapped in a using for deterministic disposal)
            using (Document src = new Document(inputPdf))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new document for each individual page
                    using (Document single = new Document())
                    {
                        // Add the current page to the new document
                        single.Pages.Add(src.Pages[i]);

                        // Build the output file name and save
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
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