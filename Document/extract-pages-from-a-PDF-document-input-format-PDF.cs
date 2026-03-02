using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedPages";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (lifecycle rule: use using for deterministic disposal)
            using (Document srcDoc = new Document(inputPath))
            {
                int pageCount = srcDoc.Pages.Count; // page indexing is 1‑based

                // Iterate over each page and save it as an individual PDF
                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a new empty document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the i‑th page from the source document
                        // (Pages.Add clones the page into the target document)
                        singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build the output file name and save
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
                        singlePageDoc.Save(outPath); // saving PDF does not require explicit SaveOptions
                        Console.WriteLine($"Saved page {i} → {outPath}");
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