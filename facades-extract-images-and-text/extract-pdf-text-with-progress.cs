using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "large_input.pdf";
        const string outputFolder  = "ExtractedPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (required to obtain total page count)
        using (Document doc = new Document(inputPdfPath))
        {
            int totalPages = doc.Pages.Count;

            // Initialize PdfExtractor with the loaded document
            PdfExtractor extractor = new PdfExtractor(doc);

            // Extract text using Unicode encoding
            extractor.ExtractText(Encoding.Unicode);

            int currentPage = 1;
            while (extractor.HasNextPageText())
            {
                // Report progress to console
                Console.WriteLine($"Processing page {currentPage} of {totalPages} ({(currentPage * 100) / totalPages}% done)");

                // Save the extracted text of the current page to a file
                string outputFile = Path.Combine(outputFolder, $"Page_{currentPage}.txt");
                extractor.GetNextPageText(outputFile);

                currentPage++;
            }

            // Clean up the extractor
            extractor.Close();
        }

        Console.WriteLine("Text extraction completed.");
    }
}