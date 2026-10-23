using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Directory where individual pages will be saved
        const string outputDir = "SplitPages";

        // Verify the source file exists
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
            using (Document src = new Document(inputPdf))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new empty PDF document for the single page
                    using (Document single = new Document())
                    {
                        // Add the i‑th page from the source document
                        single.Pages.Add(src.Pages[i]);

                        // Build the output file name (e.g., Page_1.pdf)
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF
                        single.Save(outPath);

                        Console.WriteLine($"Saved page {i} → {outPath}");
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