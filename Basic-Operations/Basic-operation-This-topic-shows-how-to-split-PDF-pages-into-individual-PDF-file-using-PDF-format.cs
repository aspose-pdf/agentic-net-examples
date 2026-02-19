using System;
using System.IO;
using Aspose.Pdf;

class SplitPdfPages
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory for individual pages
        const string outputDirectory = "output_pages";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the source PDF document
            Document sourceDocument = new Document(inputPdfPath);

            // Iterate through each page (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= sourceDocument.Pages.Count; pageIndex++)
            {
                // Create a new empty PDF document
                Document singlePageDocument = new Document();

                // Add the current page from the source document to the new document
                // The Add method copies the page, preserving its content and resources
                singlePageDocument.Pages.Add(sourceDocument.Pages[pageIndex]);

                // Build the output file name (e.g., page_1.pdf, page_2.pdf, …)
                string outputPath = Path.Combine(outputDirectory, $"page_{pageIndex}.pdf");

                // Save the single‑page PDF using the provided document-save rule
                singlePageDocument.Save(outputPath);

                Console.WriteLine($"Saved page {pageIndex} to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}