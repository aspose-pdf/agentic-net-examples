using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF inside a using block for proper disposal
            using (Document src = new Document(inputPdf))
            {
                // Pages are 1‑based; iterate from 1 to Count inclusive
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new empty document for each page
                    using (Document single = new Document())
                    {
                        // Add the current page from the source document
                        single.Pages.Add(src.Pages[i]);

                        // Build the output file name and save the single‑page PDF
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
                        single.Save(outPath);
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF split completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}