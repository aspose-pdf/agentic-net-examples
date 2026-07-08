using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "large_document.pdf";   // source PDF
        const string outputRoot   = "SplitPages";          // root folder for page folders

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the source PDF (wrapped in using for deterministic disposal)
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int pageNumber = 1; pageNumber <= sourceDoc.Pages.Count; pageNumber++)
                {
                    // Create a fresh document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page to the new document
                        singlePageDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                        // Prepare a folder for this page (e.g., "SplitPages/Page_1")
                        string pageFolder = Path.Combine(outputRoot, $"Page_{pageNumber}");
                        Directory.CreateDirectory(pageFolder);

                        // Save the single‑page PDF inside its folder
                        string outputPdfPath = Path.Combine(pageFolder, $"Page_{pageNumber}.pdf");
                        singlePageDoc.Save(outputPdfPath);
                        Console.WriteLine($"Saved page {pageNumber} to '{outputPdfPath}'");
                    }
                }
            }

            Console.WriteLine("Batch split completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}