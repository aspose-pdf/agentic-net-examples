using System;
using System.IO;
using Aspose.Pdf;

class ExportSelectedPages
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";

        // Folder where individual pages will be saved
        const string outputFolder = "ExportedPages";

        // Page numbers to export (1‑based indexing)
        int[] selectedPages = { 1, 3, 5 }; // example: export pages 1, 3 and 5

        // Validate input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the source PDF (using block ensures proper disposal)
            using (Document sourceDoc = new Document(inputPath))
            {
                int totalPages = sourceDoc.Pages.Count;

                foreach (int pageNumber in selectedPages)
                {
                    // Validate page number (Aspose.Pdf uses 1‑based indexing)
                    if (pageNumber < 1 || pageNumber > totalPages)
                    {
                        Console.Error.WriteLine($"Page {pageNumber} is out of range (1‑{totalPages}). Skipping.");
                        continue;
                    }

                    // Create a new PDF document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the selected page; this copies the page preserving size and orientation
                        singlePageDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                        // Build output file name
                        string outputPath = Path.Combine(
                            outputFolder,
                            $"{Path.GetFileNameWithoutExtension(inputPath)}_page_{pageNumber}.pdf");

                        // Save the single‑page PDF
                        singlePageDoc.Save(outputPath);
                        Console.WriteLine($"Exported page {pageNumber} to '{outputPath}'.");
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