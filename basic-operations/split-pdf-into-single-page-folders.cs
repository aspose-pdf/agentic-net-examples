using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "large_input.pdf";          // source PDF
        const string outputRoot = "SplitPages";               // root folder for all pages

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the source PDF (using block ensures proper disposal)
            using (Document srcDoc = new Document(inputPdf))
            {
                // Iterate using 1‑based page indexing (Aspose.Pdf requirement)
                for (int pageNumber = 1; pageNumber <= srcDoc.Pages.Count; pageNumber++)
                {
                    // Create a folder for the current page (e.g., SplitPages/Page_1)
                    string pageFolder = Path.Combine(outputRoot, $"Page_{pageNumber}");
                    Directory.CreateDirectory(pageFolder);

                    // Create a new PDF document containing only the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page from the source document
                        singlePageDoc.Pages.Add(srcDoc.Pages[pageNumber]);

                        // Save the single‑page PDF inside its folder
                        string outputPath = Path.Combine(pageFolder, $"page_{pageNumber}.pdf");
                        singlePageDoc.Save(outputPath);
                        Console.WriteLine($"Saved page {pageNumber} to '{outputPath}'");
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