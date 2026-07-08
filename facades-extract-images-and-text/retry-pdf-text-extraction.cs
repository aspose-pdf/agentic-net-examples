using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
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
                // PdfExtractor implements IDisposable, so use a using block
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the source PDF file
                    extractor.BindPdf(inputPdf);

                    // Extract all text using Unicode encoding (default)
                    extractor.ExtractText();

                    // Save extracted text to the output file
                    extractor.GetText(outputTxt);
                }

                // If we reach here, extraction succeeded
                success = true;
                Console.WriteLine($"Extraction succeeded on attempt {attempt}.");
            }
            catch (IOException ioEx)
            {
                // Log the I/O error and retry if attempts remain
                Console.Error.WriteLine($"I/O error on attempt {attempt}: {ioEx.Message}");
                if (attempt >= maxAttempts)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Extraction failed.");
                }
                else
                {
                    // Optional: brief pause before retry
                    System.Threading.Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                // Non‑IO exceptions are not retried
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }
    }
}