using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "SplitPages";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the source PDF inside a using block for deterministic disposal
            using (Document src = new Document(inputPdf))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new empty PDF document for the single page
                    using (Document single = new Document())
                    {
                        // Add the current page from the source document
                        single.Pages.Add(src.Pages[i]);

                        // Build the output file name
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF
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