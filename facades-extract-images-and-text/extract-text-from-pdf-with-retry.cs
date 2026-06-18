using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Added for PdfException

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        const int maxAttempts = 3;
        int attempt = 0;
        bool success = false;

        while (attempt < maxAttempts && !success)
        {
            attempt++;
            try
            {
                // Create and use PdfExtractor within a using block for deterministic disposal
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the source PDF file
                    extractor.BindPdf(inputPdf);

                    // Extract all text (default pure text mode)
                    extractor.ExtractText();

                    // Save extracted text to a file
                    extractor.GetText(outputTxt);
                }

                success = true;
                Console.WriteLine($"Extraction succeeded on attempt {attempt}.");
            }
            catch (IOException ioEx)
            {
                // Retry on I/O errors (e.g., file lock, temporary read failure)
                Console.Error.WriteLine($"IOException on attempt {attempt}: {ioEx.Message}");
                if (attempt >= maxAttempts)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Extraction failed.");
                }
            }
            catch (PdfException pdfEx)
            {
                // Non-recoverable PDF errors – abort retries
                Console.Error.WriteLine($"PdfException: {pdfEx.Message}");
                break;
            }
        }
    }
}
