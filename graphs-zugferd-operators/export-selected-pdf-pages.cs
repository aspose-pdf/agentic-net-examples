using System;
using System.IO;
using Aspose.Pdf;

class ExportSelectedPages
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Folder where individual page PDFs will be saved
        const string outputFolder = "ExportedPages";

        // List of pages to export (1‑based page numbers)
        int[] selectedPages = { 1, 3, 5 }; // example: export pages 1, 3 and 5

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the source PDF (using block ensures proper disposal)
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                // Verify that requested pages exist in the source document
                int totalPages = sourceDoc.Pages.Count;
                foreach (int pageNum in selectedPages)
                {
                    if (pageNum < 1 || pageNum > totalPages)
                    {
                        Console.Error.WriteLine($"Page number {pageNum} is out of range (1‑{totalPages}). Skipping.");
                        continue;
                    }

                    // Create a new PDF document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the selected page; this copies the page together with its size and orientation
                        singlePageDoc.Pages.Add(sourceDoc.Pages[pageNum]);

                        // Build output file name (e.g., ExportedPages/page_1.pdf)
                        string outputPath = Path.Combine(outputFolder, $"page_{pageNum}.pdf");

                        // Save the new document as PDF (no SaveOptions needed for PDF output)
                        singlePageDoc.Save(outputPath);

                        Console.WriteLine($"Exported page {pageNum} to '{outputPath}'.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}