using System;
using System.IO;
using Aspose.Pdf;

class SplitPdfExample
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";

        // Output directory for split pages
        const string outputDir = "output_pages";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF document
            Document sourceDoc = new Document(inputPath);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= sourceDoc.Pages.Count; i++)
            {
                // Create a new empty PDF document
                Document singlePageDoc = new Document();

                // Add the current page from the source document to the new document
                // The Add method copies the page, leaving the source unchanged
                singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                // Build the output file name (e.g., page_1.pdf, page_2.pdf, ...)
                string outputPath = Path.Combine(outputDir, $"page_{i}.pdf");

                // Save the single‑page PDF using the standard save rule
                singlePageDoc.Save(outputPath);

                Console.WriteLine($"Saved page {i} to '{outputPath}'.");
            }

            Console.WriteLine("PDF splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}