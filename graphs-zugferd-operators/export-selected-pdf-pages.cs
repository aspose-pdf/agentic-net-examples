using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";

        // Directory where individual pages will be saved
        const string outputDir = "ExportedPages";

        // Example list of pages to export (1‑based indexing)
        int[] pagesToExport = { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (wrapped in using for deterministic disposal)
            using (Document srcDoc = new Document(inputPath))
            {
                foreach (int pageNum in pagesToExport)
                {
                    // Validate page number (Aspose.Pdf uses 1‑based indexing)
                    if (pageNum < 1 || pageNum > srcDoc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Page {pageNum} is out of range.");
                        continue;
                    }

                    // Create a new document for the single page
                    using (Document singleDoc = new Document())
                    {
                        // Add the selected page; this copies the page together with its size and orientation
                        singleDoc.Pages.Add(srcDoc.Pages[pageNum]);

                        // Build the output file name
                        string outPath = Path.Combine(outputDir, $"Page_{pageNum}.pdf");

                        // Save the single‑page PDF
                        singleDoc.Save(outPath);
                        Console.WriteLine($"Saved page {pageNum} → {outPath}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}