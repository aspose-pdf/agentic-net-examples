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

        // Retry up to three times if an IOException occurs during extraction
        const int maxAttempts = 3;
        int attempt = 0;
        bool extracted = false;

        while (attempt < maxAttempts && !extracted)
        {
            attempt++;
            // PdfExtractor implements IDisposable, so use a using block for deterministic cleanup
            using (PdfExtractor extractor = new PdfExtractor())
            {
                try
                {
                    // Load the PDF document
                    extractor.BindPdf(inputPdf);

                    // Perform the extraction
                    extractor.ExtractText();

                    // Save the extracted text to a file
                    extractor.GetText(outputTxt);

                    extracted = true; // success
                }
                catch (IOException ioEx)
                {
                    Console.Error.WriteLine($"Attempt {attempt} failed with IOException: {ioEx.Message}");
                    if (attempt >= maxAttempts)
                    {
                        Console.Error.WriteLine("Maximum retry attempts reached. Extraction aborted.");
                        // Optionally rethrow or handle as needed
                    }
                }
                finally
                {
                    // Ensure the facade releases any resources it holds
                    extractor.Close();
                }
            }
        }

        if (extracted)
        {
            Console.WriteLine($"Text extraction succeeded. Output saved to '{outputTxt}'.");
        }
    }
}