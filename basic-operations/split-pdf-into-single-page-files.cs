using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Directory where individual pages will be saved
        const string outputDir = "SplitPages";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (load rule)
            using (Document srcDoc = new Document(inputPath))
            {
                int pageCount = srcDoc.Pages.Count; // Pages are 1‑based (global rule)

                // Iterate over each page and create a separate PDF (loop + split logic)
                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a new empty PDF document (create rule)
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page from the source document
                        singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build the output file name
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF (save rule)
                        singlePageDoc.Save(outPath);

                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}