using System;
using System.IO;
using Aspose.Pdf;

class SplitPdfIntoPages
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory where individual page PDFs will be saved
        const string outputDirectory = "SplitPages";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the source PDF document inside a using block for deterministic disposal
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int pageNumber = 1; pageNumber <= sourceDoc.Pages.Count; pageNumber++)
                {
                    // Create a new empty PDF document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page from the source document to the new document
                        // The Add method copies the page reference; the source document remains unchanged
                        singlePageDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                        // Build the output file name (e.g., Page_1.pdf, Page_2.pdf, ...)
                        string outputPath = Path.Combine(outputDirectory, $"Page_{pageNumber}.pdf");

                        // Save the single‑page PDF; no SaveOptions needed because the format is PDF
                        singlePageDoc.Save(outputPath);

                        Console.WriteLine($"Saved page {pageNumber} → {outputPath}");
                    }
                }
            }

            Console.WriteLine("PDF splitting completed successfully.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors and report them
            Console.Error.WriteLine($"Error during splitting: {ex.Message}");
        }
    }
}