using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where per‑page text files will be saved
        const string outputDir = "ExtractedPages";

        // Optional: file to hold the whole document text
        const string fullTextFile = "full_text.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) to extract text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Extract text using Unicode encoding (default)
            extractor.ExtractText();

            // Save the complete text to a single file (optional)
            extractor.GetText(fullTextFile);

            // Extract each page's text into separate files
            int pageNumber = 1;
            while (extractor.HasNextPageText())
            {
                string pageTextFile = Path.Combine(outputDir, $"Page_{pageNumber}.txt");
                extractor.GetNextPageText(pageTextFile);
                pageNumber++;
            }
        }

        Console.WriteLine("Text extraction completed.");
    }
}