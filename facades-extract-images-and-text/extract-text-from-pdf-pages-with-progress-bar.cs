using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Simple console progress bar
    static void ShowProgress(int currentPage, int totalPages)
    {
        int percent = (int)((double)currentPage / totalPages * 100);
        Console.CursorLeft = 0;
        Console.Write($"Processing page {currentPage}/{totalPages} ({percent}%)");
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "ExtractedText";      // folder for per‑page text files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            int totalPages = doc.Pages.Count; // 1‑based page count

            // Initialise the extractor (lifecycle rule: use using)
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the document to the extractor
                extractor.BindPdf(doc);

                // Extract text for the whole document (Unicode encoding)
                extractor.ExtractText(Encoding.Unicode);

                int pageIndex = 1; // 1‑based page index for progress
                while (extractor.HasNextPageText())
                {
                    // Build output file name for the current page
                    string outPath = Path.Combine(outputDir, $"Page_{pageIndex}.txt");

                    // Save the extracted text of the current page
                    extractor.GetNextPageText(outPath);

                    // Update progress UI
                    ShowProgress(pageIndex, totalPages);
                    pageIndex++;
                }
            }

            Console.WriteLine(); // move to next line after progress bar
        }

        Console.WriteLine("Text extraction completed.");
    }
}