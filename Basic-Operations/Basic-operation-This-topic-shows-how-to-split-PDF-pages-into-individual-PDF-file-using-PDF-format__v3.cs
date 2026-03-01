using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string inputPdf = "input.pdf";

        // Directory where individual pages will be saved.
        const string outputDir = "SplitPages";

        // Verify that the source file exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF document.
            using (Document src = new Document(inputPdf))
            {
                // Iterate over all pages (1‑based indexing).
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new empty PDF document for the current page.
                    using (Document single = new Document())
                    {
                        // Add the current page from the source document.
                        single.Pages.Add(src.Pages[i]);

                        // Build the output file name.
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF.
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