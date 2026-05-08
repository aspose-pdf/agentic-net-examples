using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class ExportSelectedPages
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Directory where individual pages will be saved
        const string outputDirectory = "ExportedPages";

        // Page numbers to export (1‑based indexing)
        int[] pagesToExport = { 1, 3, 5 }; // example: export pages 1, 3 and 5

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the source PDF (wrapped in using for deterministic disposal)
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                // Verify requested pages are within the document range
                int totalPages = sourceDoc.Pages.Count;
                foreach (int pageNum in pagesToExport)
                {
                    if (pageNum < 1 || pageNum > totalPages)
                    {
                        Console.Error.WriteLine($"Page number {pageNum} is out of range (1‑{totalPages}). Skipping.");
                        continue;
                    }

                    // Create a new PDF containing only the selected page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page from the source document.
                        // This preserves the original page size and orientation.
                        singlePageDoc.Pages.Add(sourceDoc.Pages[pageNum]);

                        // Build output file name
                        string outputPath = Path.Combine(
                            outputDirectory,
                            $"page_{pageNum}.pdf");

                        // Save the single‑page PDF
                        singlePageDoc.Save(outputPath);
                        Console.WriteLine($"Exported page {pageNum} to '{outputPath}'.");
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