using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

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
            // Load source PDF (wrapped in using for deterministic disposal)
            using (Document src = new Document(inputPath))
            {
                // Iterate pages using 1‑based indexing
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new PDF for the single page
                    using (Document single = new Document())
                    {
                        // Add the current page from the source document
                        single.Pages.Add(src.Pages[i]);

                        // Build output file name and save
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
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