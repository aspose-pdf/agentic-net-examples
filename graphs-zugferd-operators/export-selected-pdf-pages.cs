using System;
using System.IO;
using Aspose.Pdf;

class ExportSelectedPages
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";

        // Pages to export (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToExport = { 2, 4, 7 }; // example page numbers

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source document inside a using block for deterministic disposal
        using (Document sourceDoc = new Document(inputPath))
        {
            // Iterate over the requested page numbers
            foreach (int pageNumber in pagesToExport)
            {
                // Validate page number range
                if (pageNumber < 1 || pageNumber > sourceDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNumber} is out of range. Skipping.");
                    continue;
                }

                // Create a new empty document for the single page
                using (Document singlePageDoc = new Document())
                {
                    // Add the selected page to the new document.
                    // The page is copied preserving its size, rotation, and other attributes.
                    singlePageDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                    // Build output file name (e.g., input_page_2.pdf)
                    string outputPath = Path.Combine(
                        Path.GetDirectoryName(inputPath) ?? string.Empty,
                        $"{Path.GetFileNameWithoutExtension(inputPath)}_page_{pageNumber}.pdf");

                    // Save the new document as PDF
                    singlePageDoc.Save(outputPath);
                    Console.WriteLine($"Exported page {pageNumber} to '{outputPath}'.");
                }
            }
        }
    }
}