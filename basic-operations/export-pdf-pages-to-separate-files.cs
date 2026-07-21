using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExportedPages";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the source PDF inside a using block for deterministic disposal
        using (Document sourceDoc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNumber = 1; pageNumber <= sourceDoc.Pages.Count; pageNumber++)
            {
                // Create a new empty PDF document for the current page
                using (Document singlePageDoc = new Document())
                {
                    // Add the page from the source document to the new document
                    // This copies the page; the source document remains unchanged
                    singlePageDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                    // Build the output file name (e.g., Page_1.pdf)
                    string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.pdf");

                    // Save the single‑page PDF
                    singlePageDoc.Save(outputPath);

                    Console.WriteLine($"Saved page {pageNumber} to '{outputPath}'.");
                }
            }
        }

        Console.WriteLine("All pages have been exported as separate PDF files.");
    }
}