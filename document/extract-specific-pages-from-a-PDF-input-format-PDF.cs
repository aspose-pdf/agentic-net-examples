using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output PDF containing only the selected pages
        const string outputPath = "extracted_pages.pdf";

        // Define which pages to extract (1‑based page numbers)
        int[] pagesToExtract = { 2, 4, 5 }; // example: extract pages 2, 4 and 5

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source document
            using (Document sourceDoc = new Document(inputPath))
            {
                // Create a new empty document for the extracted pages
                using (Document targetDoc = new Document())
                {
                    // Ensure the source document has enough pages
                    int totalPages = sourceDoc.Pages.Count;
                    foreach (int pageNum in pagesToExtract)
                    {
                        if (pageNum < 1 || pageNum > totalPages)
                        {
                            Console.Error.WriteLine($"Page number {pageNum} is out of range (1‑{totalPages}). Skipping.");
                            continue;
                        }

                        // Add the specified page to the target document
                        // Pages collection uses 1‑based indexing
                        targetDoc.Pages.Add(sourceDoc.Pages[pageNum]);
                    }

                    // Save the result as a PDF
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