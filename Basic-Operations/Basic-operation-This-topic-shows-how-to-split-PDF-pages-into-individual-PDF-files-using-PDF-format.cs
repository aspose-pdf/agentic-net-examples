using System;
using System.IO;
using Aspose.Pdf;

class SplitPdfPages
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: SplitPdfPages <input-pdf> <output-folder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPath}'.");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        try
        {
            // Load the source PDF document
            Document sourceDoc = new Document(inputPath);

            // Iterate over each page (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= sourceDoc.Pages.Count; i++)
            {
                // Create a new empty PDF document
                Document singlePageDoc = new Document();

                // Import the current page from the source document
                // The Add method copies the page, leaving the source unchanged
                singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                // Build output file name (e.g., page_1.pdf)
                string outputPath = Path.Combine(outputFolder, $"page_{i}.pdf");

                // Save the single‑page document (using the provided lifecycle rule)
                singlePageDoc.Save(outputPath);

                Console.WriteLine($"Saved page {i} to '{outputPath}'.");
            }

            Console.WriteLine("PDF splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}