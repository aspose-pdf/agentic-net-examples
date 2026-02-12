using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF
        const string inputPath = "input.pdf";

        // Directory where individual pages will be saved
        const string outputDir = "output_pages";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the original PDF document
            Document srcDoc = new Document(inputPath);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= srcDoc.Pages.Count; i++)
            {
                // Create a new empty PDF document
                Document singlePageDoc = new Document();

                // Add the current page to the new document (creates a copy)
                singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                // Build the output file name for this page
                string outPath = Path.Combine(outputDir, $"page_{i}.pdf");

                // Save the single‑page PDF (uses the document-save rule)
                singlePageDoc.Save(outPath);

                Console.WriteLine($"Saved page {i} to {outPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}