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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (lifecycle rule: use using for disposal)
            using (Document src = new Document(inputPdf))
            {
                // Iterate pages using 1‑based indexing (rule: page-indexing-one-based)
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new empty PDF document for the single page
                    using (Document single = new Document())
                    {
                        // Add the current page from the source document
                        single.Pages.Add(src.Pages[i]);

                        // Build output file name for this page
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF (lifecycle rule: use Save inside using)
                        single.Save(outPath);

                        Console.WriteLine($"Saved page {i} to '{outPath}'.");
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