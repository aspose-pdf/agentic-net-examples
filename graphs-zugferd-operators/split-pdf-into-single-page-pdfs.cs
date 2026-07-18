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
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Create a new empty PDF document for the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the i‑th page from the source document to the new document
                        singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build the output file name (e.g., Page_1.pdf)
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page document as PDF
                        singlePageDoc.Save(outPath);

                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF split completed successfully.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}