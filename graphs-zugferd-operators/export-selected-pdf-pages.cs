using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string sourcePath = "input.pdf";

        // Directory where the extracted pages will be saved
        const string outputDir = "ExtractedPages";

        // Define which pages to extract (1‑based indices)
        int[] selectedPages = { 2, 4, 7 }; // example: extract pages 2, 4 and 7

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the source document inside a using block (lifecycle rule)
        using (Document sourceDoc = new Document(sourcePath))
        {
            // Validate selected page numbers against the source document page count
            foreach (int pageNumber in selectedPages)
            {
                if (pageNumber < 1 || pageNumber > sourceDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page number {pageNumber} is out of range (1‑{sourceDoc.Pages.Count}). Skipping.");
                    continue;
                }

                // Create a new document for the single page
                using (Document singlePageDoc = new Document())
                {
                    // Add the selected page to the new document.
                    // This copies the page preserving its size and orientation.
                    singlePageDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                    // Build the output file name (e.g., Page_2.pdf)
                    string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.pdf");

                    // Save the new document as PDF (no special SaveOptions needed)
                    singlePageDoc.Save(outputPath);

                    Console.WriteLine($"Extracted page {pageNumber} to '{outputPath}'.");
                }
            }
        }

        Console.WriteLine("Page extraction completed.");
    }
}