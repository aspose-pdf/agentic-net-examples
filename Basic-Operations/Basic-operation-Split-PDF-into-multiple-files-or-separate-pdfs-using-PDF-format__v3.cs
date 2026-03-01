using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load source PDF (using block ensures proper disposal)
            using (Document srcDoc = new Document(inputPath))
            {
                // Iterate pages using 1‑based indexing
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Create a new PDF for the current page
                    using (Document singleDoc = new Document())
                    {
                        // Add the page from the source document
                        singleDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build output file path and save
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
                        singleDoc.Save(outPath);
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF split operation completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}