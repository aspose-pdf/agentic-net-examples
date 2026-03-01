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
            // Load the source PDF (lifecycle rule: use using)
            using (Document srcDoc = new Document(inputPath))
            {
                // Pages are 1‑based (global rule)
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Create a new empty PDF document (create rule)
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page to the new document
                        singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build the output file name for this page
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF (save rule)
                        singlePageDoc.Save(outPath);
                        Console.WriteLine($"Saved page {i} → '{outPath}'.");
                    }
                }
            }

            Console.WriteLine("PDF split operation completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}