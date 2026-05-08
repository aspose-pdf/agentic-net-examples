using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "SplitPages";        // folder for individual pages

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (using block ensures proper disposal)
            using (Document srcDoc = new Document(inputPdf))
            {
                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNumber = 1; pageNumber <= srcDoc.Pages.Count; pageNumber++)
                {
                    // Create a new empty PDF document for the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page from the source document to the new document
                        // Pages.Add copies the page reference; the original document remains intact
                        singlePageDoc.Pages.Add(srcDoc.Pages[pageNumber]);

                        // Build the output file name (e.g., Page_1.pdf)
                        string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.pdf");

                        // Save the single‑page PDF (no SaveOptions needed for PDF output)
                        singlePageDoc.Save(outPath);

                        Console.WriteLine($"Saved page {pageNumber} → {outPath}");
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