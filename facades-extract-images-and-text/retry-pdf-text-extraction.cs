using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTxt = "output.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        bool extracted = false;

        // Retry up to three times if an IOException occurs
        for (int attempt = 1; attempt <= 3 && !extracted; attempt++)
        {
            try
            {
                // Create and bind the extractor (lifecycle: create → bind → extract → save)
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(inputPdf);          // load PDF
                    extractor.ExtractText();              // perform extraction
                    extractor.GetText(outputTxt);         // save extracted text
                }

                extracted = true;
                Console.WriteLine($"Extraction succeeded on attempt {attempt}.");
            }
            catch (IOException ioEx) // retry only on I/O errors
            {
                Console.Error.WriteLine($"IO exception on attempt {attempt}: {ioEx.Message}");
                if (attempt == 3)
                {
                    Console.Error.WriteLine("All retry attempts failed.");
                }
                else
                {
                    // Optional short delay before next attempt
                    Thread.Sleep(500);
                }
            }
            catch (PdfException pdfEx) // non‑IO PDF errors are not retried
            {
                Console.Error.WriteLine($"PDF processing error: {pdfEx.Message}");
                break;
            }
        }
    }
}
