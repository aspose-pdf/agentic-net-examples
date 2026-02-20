using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ExtractPagesExample
{
    static void Main(string[] args)
    {
        // Input PDF path
        const string inputPdfPath = "input.pdf";
        // Output PDF that will contain the extracted pages
        const string outputPdfPath = "extracted_pages.pdf";

        // Define the page range to extract (1‑based inclusive)
        const int startPage = 2;
        const int endPage = 4;

        // Verify the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        // Ensure the page range is valid
        if (startPage < 1 || endPage < startPage)
        {
            Console.Error.WriteLine("Error: Invalid page range.");
            return;
        }

        try
        {
            // Use a Facade class (PdfFileInfo) to load the document and obtain metadata
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath))
            {
                int totalPages = pdfInfo.NumberOfPages;
                Console.WriteLine($"Source PDF has {totalPages} pages.");

                // Adjust the end page if it exceeds the document length
                int actualEnd = Math.Min(endPage, totalPages);
                if (startPage > totalPages)
                {
                    Console.Error.WriteLine("Error: Start page exceeds total page count.");
                    return;
                }

                // Load the full document using the core API (required for page manipulation)
                using (Document sourceDoc = new Document(inputPdfPath))
                {
                    // Create a new empty document for the extracted pages
                    using (Document targetDoc = new Document())
                    {
                        // Remove the default blank page that a new Document contains
                        targetDoc.Pages.Clear();

                        // Copy the selected pages from source to target
                        for (int pageNum = startPage; pageNum <= actualEnd; pageNum++)
                        {
                            // Pages collection is 1‑based
                            targetDoc.Pages.Add(sourceDoc.Pages[pageNum]);
                        }

                        // Save the result
                        targetDoc.Save(outputPdfPath);
                        Console.WriteLine($"Extracted pages saved to '{outputPdfPath}'.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}