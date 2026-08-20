using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputDir  = "ExtractedPages";    // folder for per‑page text files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable – use a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Extract all text (Unicode encoding) – required before calling GetNextPageText / HasNextPageText
            extractor.ExtractText(Encoding.Unicode);

            // Total number of pages in the document (1‑based indexing)
            int totalPages = extractor.Document.Pages.Count;

            int currentPage = 1;
            while (extractor.HasNextPageText())
            {
                // Build output file name for the current page
                string outPath = Path.Combine(outputDir, $"Page_{currentPage}.txt");

                // Save the text of the current page
                extractor.GetNextPageText(outPath);

                // Calculate and display progress percentage
                int percent = (int)((double)currentPage / totalPages * 100);
                Console.WriteLine($"Processed page {currentPage}/{totalPages} ({percent}%)");

                currentPage++;
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}