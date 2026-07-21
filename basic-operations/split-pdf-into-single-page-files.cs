using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "SplitPages";

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
            // Load the multi‑page PDF
            using (Document sourceDoc = new Document(inputPdf))
            {
                int pageCount = sourceDoc.Pages.Count; // 1‑based indexing

                // Iterate over each page and create a separate PDF
                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a new empty document for the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page from the source document
                        singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                        // Build the output file name
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF
                        singlePageDoc.Save(outPath);
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("PDF splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}