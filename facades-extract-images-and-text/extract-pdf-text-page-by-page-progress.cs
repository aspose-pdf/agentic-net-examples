using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";               // source PDF
        const string outputDir = "ExtractedPages";          // folder for per‑page text files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (facade) to extract text page by page
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Extract text for the whole document (Unicode encoding)
            extractor.ExtractText(Encoding.Unicode);

            // Determine total pages to process
            int totalPages = extractor.Document.Pages.Count;
            extractor.StartPage = 1;
            extractor.EndPage   = totalPages;

            int currentPage = 1;

            // Loop while there is more page text available
            while (extractor.HasNextPageText())
            {
                // Build output file name for the current page
                string outPath = Path.Combine(outputDir, $"page_{currentPage}.txt");

                // Save the current page's text to the file
                extractor.GetNextPageText(outPath);

                // Calculate and display progress percentage
                double percent = (double)currentPage / totalPages * 100;
                Console.Write($"\rProcessing page {currentPage}/{totalPages} ({percent:0.0}%)");

                currentPage++;
            }

            Console.WriteLine("\nExtraction completed.");
        }
    }
}