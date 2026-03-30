using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPrefix = "page";
        const string outputSuffix = ".txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to obtain the total number of pages.
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count;

            // Initialize the extractor and bind the source PDF.
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPath);
            extractor.ExtractText(Encoding.Unicode);

            int currentPage = 1;
            while (extractor.HasNextPageText())
            {
                // Report progress to the console.
                Console.WriteLine($"{DateTime.Now.ToLongTimeString()} - Extracting text from page {currentPage} of {totalPages}.");

                string outputFile = $"{outputPrefix}{currentPage}{outputSuffix}";
                extractor.GetNextPageText(outputFile);
                currentPage++;
            }
        }

        Console.WriteLine("Text extraction completed.");
    }
}