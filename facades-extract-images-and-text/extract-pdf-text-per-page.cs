using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where per‑page text files will be written
        const string outputDir = "ExtractedPages";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Create the PdfExtractor facade, bind the PDF, and extract text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document (load rule)
            extractor.BindPdf(inputPdf);

            // Perform text extraction using Unicode encoding (create rule)
            extractor.ExtractText();

            // Iterate over each page's extracted text and save it to a separate file
            int pageNumber = 1;
            while (extractor.HasNextPageText())
            {
                string pageFile = Path.Combine(outputDir, $"Page_{pageNumber}.txt");
                // Save the current page's text (save rule)
                extractor.GetNextPageText(pageFile);
                pageNumber++;
            }
        }

        Console.WriteLine("Text extraction and per‑page splitting completed.");
    }
}