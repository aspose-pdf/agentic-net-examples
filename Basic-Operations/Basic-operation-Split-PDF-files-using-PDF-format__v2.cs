using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF inside a using block (lifecycle rule)
            using (Document srcDoc = new Document(inputPdf))
            {
                // Aspose.Pdf uses 1‑based page indexing (global rule)
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Create a new empty document for each page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page to the new document
                        singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build the output file name
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF (lifecycle rule)
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