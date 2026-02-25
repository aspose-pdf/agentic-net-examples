using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
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
            // document-disposal-with-using: both source and per‑page documents are wrapped in using blocks
            using (Document src = new Document(inputPdf))
            {
                // page-indexing-one-based: Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    using (Document single = new Document())
                    {
                        // Add the i‑th page from the source document to the new document
                        single.Pages.Add(src.Pages[i]);

                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
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