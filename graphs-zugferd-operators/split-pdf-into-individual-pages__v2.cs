using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document srcDoc = new Document(inputPdf))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int pageNumber = 1; pageNumber <= srcDoc.Pages.Count; pageNumber++)
                {
                    // Create a new PDF document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the specific page from the source document
                        singlePageDoc.Pages.Add(srcDoc.Pages[pageNumber]);

                        // Build the output file name (e.g., Page_1.pdf)
                        string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.pdf");

                        // Save the single‑page PDF
                        singlePageDoc.Save(outPath);
                        Console.WriteLine($"Saved page {pageNumber} to '{outPath}'");
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