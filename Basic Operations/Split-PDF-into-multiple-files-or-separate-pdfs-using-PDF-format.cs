using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF; using ensures deterministic disposal
            using (Document srcDoc = new Document(inputPath))
            {
                // Pages collection uses 1‑based indexing
                for (int pageNum = 1; pageNum <= srcDoc.Pages.Count; pageNum++)
                {
                    // Create a new empty PDF document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page from the source document
                        singlePageDoc.Pages.Add(srcDoc.Pages[pageNum]);

                        // Construct the output file name (e.g., Page_1.pdf)
                        string outPath = Path.Combine(outputDir, $"Page_{pageNum}.pdf");

                        // Save the single‑page PDF (PDF format by default)
                        singlePageDoc.Save(outPath);

                        Console.WriteLine($"Saved page {pageNum} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF splitting completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}