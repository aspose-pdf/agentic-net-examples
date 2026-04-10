using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "large_document.pdf";
        const string outputFolder = "ExtractedPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor to process the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Retrieve total page count for progress calculation
            int totalPages = extractor.Document.Pages.Count;

            // Prepare the extractor to extract text (Unicode encoding)
            extractor.ExtractText(Encoding.Unicode);

            int currentPage = 1;
            // Loop through each page while there is more text to retrieve
            while (extractor.HasNextPageText())
            {
                // Report progress to the console
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Processing page {currentPage} of {totalPages}");

                // Save the extracted text of the current page to a file
                string outputPath = Path.Combine(outputFolder, $"Page_{currentPage}.txt");
                extractor.GetNextPageText(outputPath);

                currentPage++;
            }

            Console.WriteLine("Text extraction completed.");
        }
    }
}