using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "SplitPages";         // folder for individual pages

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source document (using ensures proper disposal)
            using (Document srcDoc = new Document(inputPdf))
            {
                // Iterate pages – Aspose.Pdf uses 1‑based indexing
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Create a new empty document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the current page from the source document
                        singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build output file name (e.g., Page_1.pdf)
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page document as PDF
                        singlePageDoc.Save(outPath);

                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during splitting: {ex.Message}");
        }
    }
}