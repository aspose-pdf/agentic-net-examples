using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF
            using (Document sourceDoc = new Document(inputPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= sourceDoc.Pages.Count; i++)
                {
                    // Create a new document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the i‑th page from the source document.
                        // The Add method internally copies the page.
                        singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
                        singlePageDoc.Save(outPath);
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF splitting completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}