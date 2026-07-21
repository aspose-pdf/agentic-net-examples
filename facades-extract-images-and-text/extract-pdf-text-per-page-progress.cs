using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the large PDF file
        const string inputPdfPath = "large_input.pdf";

        // Directory where per‑page text files will be saved
        const string outputDirectory = "ExtractedPages";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // ------------------------------------------------------------
        // Determine total number of pages for progress reporting.
        // Use the standard Document loading rule (inside a using block).
        // ------------------------------------------------------------
        int totalPages;
        using (Document doc = new Document(inputPdfPath))
        {
            totalPages = doc.Pages.Count; // 1‑based page count
        }

        // ------------------------------------------------------------
        // Set up the PdfExtractor (also disposable) and bind the PDF.
        // ------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);

            // Extract text using Unicode encoding (required before calling GetNextPageText)
            extractor.ExtractText(Encoding.Unicode);

            int currentPage = 1;

            // Loop while there is more page text to retrieve
            while (extractor.HasNextPageText())
            {
                // Build the output file name for the current page
                string pageTextPath = Path.Combine(outputDirectory, $"Page_{currentPage}.txt");

                // Save the text of the current page to the file
                extractor.GetNextPageText(pageTextPath);

                // Report progress to the console
                Console.WriteLine($"Processed page {currentPage} of {totalPages}");

                currentPage++;
            }
        }

        Console.WriteLine("Text extraction completed.");
    }
}