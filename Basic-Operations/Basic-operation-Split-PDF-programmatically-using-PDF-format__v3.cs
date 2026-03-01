using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Directory where split pages will be saved
        const string outputDir = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load source PDF (wrapped in using for proper disposal)
            using (Document src = new Document(inputPdf))
            {
                // Iterate pages using 1‑based indexing
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new empty document for the single page
                    using (Document single = new Document())
                    {
                        // Add the current page to the new document
                        single.Pages.Add(src.Pages[i]);

                        // Build output file path for this page
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF (extension .pdf ensures PDF output)
                        single.Save(outPath);

                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("Splitting complete.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}