using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfExtractionWithProgress
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output folder where each page's text will be saved
        const string outputFolder = "ExtractedPages";

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the document to obtain the total page count (required for progress calculation)
        using (Document doc = new Document(inputPdf))
        {
            int totalPages = doc.Pages.Count;
            Console.WriteLine($"Total pages to process: {totalPages}");

            // Initialize the PdfExtractor facade
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(inputPdf);

                // Extract text for the whole document (Unicode encoding is default)
                extractor.ExtractText();

                int currentPage = 1;
                // Loop through each page's extracted text
                while (extractor.HasNextPageText())
                {
                    // Build output file name for the current page
                    string outputPath = Path.Combine(outputFolder, $"Page_{currentPage}.txt");

                    // Save the extracted text of the current page
                    extractor.GetNextPageText(outputPath);

                    // Calculate and display progress percentage
                    int percent = (int)((double)currentPage / totalPages * 100);
                    Console.WriteLine($"Processed page {currentPage}/{totalPages} ({percent}%)");

                    currentPage++;
                }
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}