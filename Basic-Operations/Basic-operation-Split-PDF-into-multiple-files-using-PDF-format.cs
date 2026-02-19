using System;
using System.IO;
using Aspose.Pdf;

class SplitPdfExample
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output folder (second argument)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: SplitPdfExample <input-pdf> <output-folder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Validate input file
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: Input file not found – {inputPath}");
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
            Document srcDoc = new Document(inputPath);

            // Iterate over each page (1‑based indexing)
            for (int i = 1; i <= srcDoc.Pages.Count; i++)
            {
                // Create a new empty PDF document
                Document singlePageDoc = new Document();

                // Remove the default empty page
                singlePageDoc.Pages.Clear();

                // Add the current page from the source document
                // The page is cloned automatically when added to another document
                singlePageDoc.Pages.Add(srcDoc.Pages[i]);

                // Build output file name (e.g., "output_1.pdf")
                string outputPath = Path.Combine(outputFolder, $"output_{i}.pdf");

                // Save the single‑page PDF (uses the provided document-save rule)
                singlePageDoc.Save(outputPath);

                Console.WriteLine($"Page {i} saved to {outputPath}");
            }

            Console.WriteLine("PDF split operation completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}