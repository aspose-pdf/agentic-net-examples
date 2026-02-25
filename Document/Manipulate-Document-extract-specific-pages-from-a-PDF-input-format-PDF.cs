using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf contains Document, Page, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "extracted_pages.pdf";

        // Define which pages to extract (1‑based page numbers)
        int[] pagesToExtract = { 2, 4, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load source PDF inside a using block for deterministic disposal
            using (Document srcDoc = new Document(inputPath))
            {
                // Create a new empty PDF document to hold the extracted pages
                using (Document destDoc = new Document())
                {
                    // Iterate over the requested page numbers
                    foreach (int pageNum in pagesToExtract)
                    {
                        // Ensure the page number is within the source document range
                        if (pageNum < 1 || pageNum > srcDoc.Pages.Count)
                        {
                            Console.Error.WriteLine($"Page {pageNum} is out of range. Skipping.");
                            continue;
                        }

                        // Add the specified page to the destination document
                        // Aspose.Pdf uses 1‑based indexing for Pages collection
                        destDoc.Pages.Add(srcDoc.Pages[pageNum]);
                    }

                    // Save the result as a PDF
                    destDoc.Save(outputPath);
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