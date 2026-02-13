using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path
        string inputPath = "input.pdf";
        // Output PDF path (contains only the extracted pages)
        string outputPath = "extracted_pages.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF document
            Document sourceDoc = new Document(inputPath);

            // Define the pages to extract (1‑based page numbers)
            int[] pagesToExtract = new int[] { 2, 4, 5 };

            // Create a new empty PDF document
            Document resultDoc = new Document();

            // Add the selected pages to the new document
            foreach (int pageNumber in pagesToExtract)
            {
                // Ensure the requested page exists in the source document
                if (pageNumber < 1 || pageNumber > sourceDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Warning: Page {pageNumber} is out of range and will be skipped.");
                    continue;
                }

                // Add the page (the page object is cloned into the target document)
                resultDoc.Pages.Add(sourceDoc.Pages[pageNumber]);
            }

            // Save the resulting PDF using the standard save rule
            resultDoc.Save(outputPath);
            Console.WriteLine($"Successfully extracted pages to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}