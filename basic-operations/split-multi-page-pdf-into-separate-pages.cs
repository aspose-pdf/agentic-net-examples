using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the multi‑page PDF (wrapped in using for deterministic disposal)
            using (Document srcDoc = new Document(inputPath))
            {
                int pageCount = srcDoc.Pages.Count; // Pages are 1‑based

                // Iterate over each page and extract it into a separate document
                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a new empty document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add (clone) the current page from the source document into the new document
                        // Using Pages.Add avoids the ambiguous CopyTo overload that conflicts with System.MemoryExtensions.CopyTo
                        singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
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
