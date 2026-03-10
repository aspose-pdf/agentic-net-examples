using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "ExtractedPages";    // folder for extracted pages

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source document (wrapped in using for deterministic disposal)
            using (Document sourceDoc = new Document(inputPdf))
            {
                int pageCount = sourceDoc.Pages.Count; // pages are 1‑based

                // Iterate over each page and save it as a separate PDF
                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a new empty document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the i‑th page from the source document
                        // (Pages.Add copies the page into the new document)
                        singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                        // Build output file name, e.g. "Page_1.pdf"
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page document as PDF
                        singlePageDoc.Save(outPath);

                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("Page extraction completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}