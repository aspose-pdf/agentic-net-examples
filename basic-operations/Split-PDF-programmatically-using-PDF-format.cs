using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load source PDF (load rule)
            using (Document src = new Document(inputPdf))
            {
                // Pages are 1‑based (page-indexing-one-based rule)
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new empty PDF (create rule)
                    using (Document single = new Document())
                    {
                        // Add the i‑th page from source to the new document
                        single.Pages.Add(src.Pages[i]);

                        // Build output file name
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF (save rule)
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