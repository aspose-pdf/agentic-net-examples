using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF
        const string inputPath = "input.pdf";

        // Directory where split pages will be saved
        const string outputDir = "output_pages";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the original PDF document
        Document sourceDocument = new Document(inputPath);

        // Iterate through each page (Aspose.Pdf collections are 1‑based)
        for (int pageNumber = 1; pageNumber <= sourceDocument.Pages.Count; pageNumber++)
        {
            // Create a new empty PDF document
            Document singlePageDocument = new Document();

            // Add the current page from the source document
            // This copies the page into the new document
            singlePageDocument.Pages.Add(sourceDocument.Pages[pageNumber]);

            // Build the output file name for this page
            string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.pdf");

            // Save the single‑page PDF (uses the provided document-save rule)
            singlePageDocument.Save(outputPath);
        }

        Console.WriteLine("PDF split operation completed successfully.");
    }
}