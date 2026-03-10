using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string inputPath = "input.pdf";

        // Directory where individual page PDFs will be saved.
        const string outputDir = "SplitPages";

        // Verify that the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF document (lifecycle rule: use using for disposal).
            using (Document srcDoc = new Document(inputPath))
            {
                // Iterate over pages using 1‑based indexing (rule: page-indexing-one-based).
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Create a new empty PDF document for the current page.
                    using (Document singleDoc = new Document())
                    {
                        // Add the i‑th page from the source document to the new document.
                        singleDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build the output file name for this page.
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                        // Save the single‑page PDF (rule: Document.Save(string) for PDF format).
                        singleDoc.Save(outPath);

                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}