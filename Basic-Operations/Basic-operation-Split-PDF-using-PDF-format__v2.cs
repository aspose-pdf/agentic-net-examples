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
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document srcDoc = new Document(inputPdf))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int pageNum = 1; pageNum <= srcDoc.Pages.Count; pageNum++)
                {
                    // Create a new empty document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page to the new document
                        singlePageDoc.Pages.Add(srcDoc.Pages[pageNum]);

                        // Build output file name (e.g., Page_1.pdf)
                        string outPath = Path.Combine(outputDir, $"Page_{pageNum}.pdf");

                        // Save the single‑page PDF
                        singlePageDoc.Save(outPath);
                        Console.WriteLine($"Saved page {pageNum} → {outPath}");
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