using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Pages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the source PDF document
        using (Document srcDoc = new Document(inputPath))
        {
            int pageCount = srcDoc.Pages.Count; // 1‑based page count

            // Iterate over each page (1‑based indexing)
            for (int i = 1; i <= pageCount; i++)
            {
                // Create a new empty PDF document for the current page
                using (Document singlePageDoc = new Document())
                {
                    // Add the current page to the new document
                    singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                    // Build the output file name for this page
                    string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                    // Save the single‑page PDF (PDF format does not require SaveOptions)
                    singlePageDoc.Save(outPath);
                    Console.WriteLine($"Saved page {i} → {outPath}");
                }
            }
        }
    }
}