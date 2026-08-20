using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the multi‑page PDF
            using (Document sourceDoc = new Document(inputPath))
            {
                int pageCount = sourceDoc.Pages.Count; // 1‑based indexing

                // Iterate over each page and create a separate PDF
                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a new empty document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page from the source document
                        singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                        // Build output file name and save
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
                        singlePageDoc.Save(outPath);

                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("Splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}