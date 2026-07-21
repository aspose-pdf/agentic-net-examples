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

        // Retry loop – up to three attempts if an IOException occurs
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

                    // Extract all text (Unicode encoding is default)
                    extractor.ExtractText();

                    // Save extracted text to a file
                    extractor.GetText(outputTxt);
                }

                Console.WriteLine($"Text extraction succeeded on attempt {attempt}.");
                success = true;
            }
            catch (IOException ioEx)
            {
                // Log the I/O error and retry if attempts remain
                Console.Error.WriteLine($"IOException on attempt {attempt}: {ioEx.Message}");
                if (attempt >= maxAttempts)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Extraction failed.");
                }
                else
                {
                    Console.WriteLine("Retrying extraction...");
                }
            }
            catch (Exception ex)
            {
                // Any other exception is not retried
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }
    }
}