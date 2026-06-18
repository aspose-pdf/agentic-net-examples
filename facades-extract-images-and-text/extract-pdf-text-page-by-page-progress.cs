using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "large.pdf";
        const string outputFolder = "ExtractedPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // Extract text using Unicode encoding
            extractor.ExtractText(Encoding.Unicode);

            int pageNumber = 1;

            // Process each page sequentially
            while (extractor.HasNextPageText())
            {
                // Save the text of the current page to a separate .txt file
                string outputFile = Path.Combine(outputFolder, $"Page_{pageNumber}.txt");
                extractor.GetNextPageText(outputFile);

                // Report progress to the console
                Console.WriteLine($"{DateTime.Now:T} - Processed page {pageNumber}");

                pageNumber++;
            }
        }

        Console.WriteLine("Text extraction completed.");
    }
}