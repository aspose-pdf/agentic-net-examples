using System;
using System.IO;
using Aspose.Pdf;

class ExtractPagesExample
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "extracted_pages.pdf";

        try
        {
            // Verify that the source file exists
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input file '{inputPath}' was not found.");
                return;
            }

            // Load the source PDF document
            Document sourceDoc = new Document(inputPath);
            int totalPages = sourceDoc.Pages.Count;

            if (totalPages == 0)
            {
                Console.WriteLine("Error: The source PDF does not contain any pages.");
                return;
            }

            // Create a new empty PDF document
            Document extractedDoc = new Document();
            // Remove the default empty page that a new Document contains (safe even if count is 0)
            extractedDoc.Pages.Clear();

            // Define the range of pages to extract (example: pages 2 to 4)
            int startPage = 2;
            int endPage   = 4;

            // Clamp the range to the actual page count of the source document
            if (startPage < 1) startPage = 1;
            if (startPage > totalPages) startPage = totalPages;
            if (endPage < startPage) endPage = startPage;
            if (endPage > totalPages) endPage = totalPages;

            // Copy each page from the source to the new document (Aspose uses 1‑based indexing)
            for (int i = startPage; i <= endPage; i++)
            {
                extractedDoc.Pages.Add(sourceDoc.Pages[i]);
            }

            // Save the extracted pages as a new PDF file
            extractedDoc.Save(outputPath);
            Console.WriteLine($"Pages {startPage}-{endPage} extracted successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}