using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for extraction

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF file
        const string outputDir  = "ExtractedPages";    // folder for per‑page text files

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (facade) to extract text page by page
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Prepare the extractor to work with text
            extractor.ExtractText();   // Unicode extraction by default

            int pageNumber = 1;
            // Loop while there is more page text available
            while (extractor.HasNextPageText())
            {
                // Build output file name for the current page
                string pageFile = Path.Combine(outputDir, $"Page_{pageNumber}.txt");

                // Save the current page's text to the file
                extractor.GetNextPageText(pageFile);

                Console.WriteLine($"Extracted page {pageNumber} to '{pageFile}'");
                pageNumber++;
            }
        }

        Console.WriteLine("Text extraction completed.");
    }
}