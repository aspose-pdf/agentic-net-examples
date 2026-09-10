using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "large.pdf";
        const string outputDir = "ExtractedPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Determine total number of pages using Document (disposed via using)
        int totalPages;
        using (Document doc = new Document(inputPdf))
        {
            totalPages = doc.Pages.Count;
        }

        // Use PdfExtractor to extract text page by page with progress reporting
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractText(Encoding.Unicode);

            int pageIndex = 1;
            while (extractor.HasNextPageText())
            {
                // Save text of the current page to a separate file
                string outPath = Path.Combine(outputDir, $"Page_{pageIndex}.txt");
                extractor.GetNextPageText(outPath);

                // Calculate and display progress
                double percent = (double)pageIndex / totalPages * 100;
                Console.WriteLine($"{DateTime.Now:T} - Processed page {pageIndex}/{totalPages} ({percent:F2}%)");

                pageIndex++;
            }
        }

        Console.WriteLine("Text extraction completed.");
    }
}