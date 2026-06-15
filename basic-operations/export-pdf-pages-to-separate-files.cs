using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";               // source PDF
        const string outputDir = "SplitPages";               // folder for individual pages

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the source document inside a using block for deterministic disposal
        using (Document sourceDoc = new Document(inputPdf))
        {
            // Iterate using 1‑based page indexing (Aspose.Pdf uses 1‑based indexes)
            for (int i = 1; i <= sourceDoc.Pages.Count; i++)
            {
                // Create a new empty document for the current page
                using (Document singlePageDoc = new Document())
                {
                    // Add the i‑th page from the source document
                    singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                    // Build the output file name (e.g., Page_1.pdf)
                    string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                    // Save the single‑page document as PDF
                    singlePageDoc.Save(outPath);
                    Console.WriteLine($"Saved page {i} → {outPath}");
                }
            }
        }

        Console.WriteLine("All pages have been exported as separate PDF files.");
    }
}