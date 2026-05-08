using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF
            using (Document sourceDoc = new Document(inputPdf))
            {
                int pageCount = sourceDoc.Pages.Count; // Pages are 1‑based

                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a new PDF document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the i‑th page from the source document
                        // Pages.Add copies the page into the target document
                        singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                        // Build the output file name, e.g., "page_1.pdf"
                        string outPath = Path.Combine(outputDir, $"page_{i}.pdf");

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
            Console.Error.WriteLine($"Error during splitting: {ex.Message}");
        }
    }
}