using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
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

        // Load the source PDF document
        Document sourceDoc = new Document(inputPath);

        // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
        for (int i = 1; i <= sourceDoc.Pages.Count; i++)
        {
            // Create a new empty document for the current page
            Document singlePageDoc = new Document();

            // Add the current page from the source document to the new document
            // The page is cloned automatically when added to another document
            singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

            // Build the output file name (e.g., page_1.pdf, page_2.pdf, ...)
            string outputPath = Path.Combine(outputDir, $"page_{i}.pdf");

            // Save the single‑page document using the prescribed save rule
            singlePageDoc.Save(outputPath);
        }

        Console.WriteLine("PDF split into individual pages completed successfully.");
    }
}