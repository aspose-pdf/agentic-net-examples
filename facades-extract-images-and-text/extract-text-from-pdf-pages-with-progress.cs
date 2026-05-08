using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "large.pdf";
        const string outputFolder = "ExtractedPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor inside a using block to guarantee disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // Extract text using Unicode encoding
            extractor.ExtractText(Encoding.Unicode);

            // Total number of pages in the source PDF
            int totalPages = extractor.Document.Pages.Count;
            int currentPage = 1;

            // Iterate through each page's text
            while (extractor.HasNextPageText())
            {
                string outputFile = Path.Combine(outputFolder, $"Page_{currentPage}.txt");
                extractor.GetNextPageText(outputFile);

                // Report progress to the console
                Console.WriteLine($"Processed page {currentPage} of {totalPages}");

                currentPage++;
            }
        }

        Console.WriteLine("Text extraction completed.");
    }
}