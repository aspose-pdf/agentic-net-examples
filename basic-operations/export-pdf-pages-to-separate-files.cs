using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";
        // Directory where individual pages will be saved
        const string outputDir = "SplitPages";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (wrapped in using for deterministic disposal)
            using (Document src = new Document(inputPdf))
            {
                // Iterate over pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new PDF document for the single page
                    using (Document single = new Document())
                    {
                        // Add the current page from the source document
                        single.Pages.Add(src.Pages[i]);

                        // Build the output file path for this page
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF
                        single.Save(outPath);

                        Console.WriteLine($"Saved page {i} to '{outPath}'.");
                    }
                }
            }

            Console.WriteLine("All pages have been exported successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}