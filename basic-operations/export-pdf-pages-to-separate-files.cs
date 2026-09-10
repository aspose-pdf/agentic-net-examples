using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExportedPages";

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
            // Load source PDF (wrapped in using for deterministic disposal)
            using (Document sourceDoc = new Document(inputPath))
            {
                int pageCount = sourceDoc.Pages.Count; // 1‑based indexing

                // Loop through each page
                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a new PDF document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page from the source document
                        singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                        // Build output file path
                        string outputPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF
                        singlePageDoc.Save(outputPath);

                        Console.WriteLine($"Page {i} saved to '{outputPath}'.");
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