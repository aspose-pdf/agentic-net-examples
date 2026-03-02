using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF containing only the selected pages
        const string outputPath = "extracted_pages.pdf";

        // Define the pages to extract (1‑based page numbers)
        int[] pagesToExtract = { 2, 4, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source document inside a using block (ensures disposal)
            using (Document sourceDoc = new Document(inputPath))
            {
                // Create a new empty document for the extracted pages
                using (Document targetDoc = new Document())
                {
                    // Iterate over the requested page numbers
                    foreach (int pageNumber in pagesToExtract)
                    {
                        // Validate page number range
                        if (pageNumber < 1 || pageNumber > sourceDoc.Pages.Count)
                        {
                            Console.Error.WriteLine($"Page number {pageNumber} is out of range. Skipping.");
                            continue;
                        }

                        // Add the page to the target document
                        // Pages collection is 1‑based, so we can use the index directly
                        targetDoc.Pages.Add(sourceDoc.Pages[pageNumber]);
                    }

                    // Save the new document (PDF format by default)
                    targetDoc.Save(outputPath);
                }
            }

            Console.WriteLine($"Extracted pages saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}