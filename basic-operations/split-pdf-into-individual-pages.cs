using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputRoot = "SplitPages";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the source PDF (lifecycle rule: wrap in using)
            using (Document src = new Document(inputPdf))
            {
                // Aspose.Pdf uses 1‑based page indexing (global rule)
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new document for the single page
                    using (Document single = new Document())
                    {
                        // Add the i‑th page from the source
                        single.Pages.Add(src.Pages[i]);

                        // Create a dedicated folder for this page
                        string pageFolder = Path.Combine(outputRoot, $"Page_{i}");
                        Directory.CreateDirectory(pageFolder);

                        // Save the page PDF inside its folder
                        string outPath = Path.Combine(pageFolder, "page.pdf");
                        single.Save(outPath);

                        Console.WriteLine($"Saved page {i} to folder '{pageFolder}'.");
                    }
                }
            }

            Console.WriteLine("Batch splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}