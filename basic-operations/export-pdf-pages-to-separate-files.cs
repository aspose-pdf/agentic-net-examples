using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "ExportedPages";      // folder for individual pages

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
            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNumber = 1; pageNumber <= sourceDoc.Pages.Count; pageNumber++)
            {
                // Create a new empty document for the current page
                using (Document singlePageDoc = new Document())
                {
                    // Add the specific page from the source document
                    singlePageDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                    // Build the output file name (e.g., Page_1.pdf)
                    string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.pdf");

                    // Save the single‑page document as PDF
                    singlePageDoc.Save(outputPath);
                    Console.WriteLine($"Saved page {pageNumber} → {outputPath}");
                }
            }
        }

        Console.WriteLine("All pages have been exported.");
    }
}