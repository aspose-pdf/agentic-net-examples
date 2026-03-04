using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file to split
        const string inputPdf = "input.pdf";
        // Directory where individual pages will be saved
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
            // Load the source PDF (using block ensures proper disposal)
            using (Document src = new Document(inputPdf))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf rule)
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new empty PDF document for the current page
                    using (Document single = new Document())
                    {
                        // Add the i‑th page from the source document
                        single.Pages.Add(src.Pages[i]);

                        // Build the output file name for this page
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF (PDF format, no SaveOptions needed)
                        single.Save(outPath);
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("Splitting complete.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}