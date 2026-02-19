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
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        try
        {
            // Load the original PDF document
            Document pdfDocument = new Document(inputPath);
            int pageCount = pdfDocument.Pages.Count;

            // Iterate through each page (Aspose.Pdf collections are 1‑based)
            for (int i = 1; i <= pageCount; i++)
            {
                // Create a new empty PDF document
                Document singlePageDoc = new Document();

                // Add the current page from the source document
                singlePageDoc.Pages.Add(pdfDocument.Pages[i]);

                // Build the output file name
                string outPath = Path.Combine(outputDir, $"page_{i}.pdf");

                // Save the single‑page PDF (uses the provided save rule)
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