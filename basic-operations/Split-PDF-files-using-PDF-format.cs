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
            // Load source PDF (document-disposal-with-using)
            using (Document src = new Document(inputPdf))
            {
                // Iterate pages using 1‑based indexing (page-indexing-one-based)
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    // Create a new document for each page
                    using (Document single = new Document())
                    {
                        // Add the current page to the new document
                        single.Pages.Add(src.Pages[i]);

                        // Build output file path
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF (PDF format)
                        single.Save(outPath);

                        Console.WriteLine($"Saved page {i} → {outPath}");
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