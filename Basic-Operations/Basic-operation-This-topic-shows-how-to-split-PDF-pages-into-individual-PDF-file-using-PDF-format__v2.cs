using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = "input.pdf";

        // Directory where individual page PDFs will be saved
        const string outputDir = "SplitPages";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document srcDoc = new Document(inputPdf))
            {
                // Iterate over pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                for (int pageIndex = 1; pageIndex <= srcDoc.Pages.Count; pageIndex++)
                {
                    // Create a new empty PDF document for the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page from the source document to the new document
                        singlePageDoc.Pages.Add(srcDoc.Pages[pageIndex]);

                        // Build the output file name (e.g., Page_1.pdf, Page_2.pdf, ...)
                        string outPath = Path.Combine(outputDir, $"Page_{pageIndex}.pdf");

                        // Save the single‑page PDF
                        singlePageDoc.Save(outPath);

                        Console.WriteLine($"Saved page {pageIndex} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF splitting completed successfully.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}