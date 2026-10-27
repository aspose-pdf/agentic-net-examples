using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "extracted.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        const int maxRetries = 3;
        int attempt = 0;
        bool extracted = false;

        while (attempt < maxRetries && !extracted)
        {
            attempt++;
            try
            {
                // PdfExtractor implements IDisposable, so wrap it in a using block
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the source PDF
                    extractor.BindPdf(inputPdfPath);

                    // Perform text extraction
                    extractor.ExtractText();

                    // Write extracted text to a file
                    using (FileStream outStream = new FileStream(outputTxtPath, FileMode.Create, FileAccess.Write))
                    {
                        extractor.GetText(outStream);
                    }
                }

                // If we reach here, extraction succeeded
                extracted = true;
                Console.WriteLine($"Extraction succeeded on attempt {attempt}.");
            }
            catch (IOException ioEx)
            {
                // Log the IOException and retry if attempts remain
                Console.Error.WriteLine($"Attempt {attempt} failed with IOException: {ioEx.Message}");
                if (attempt >= maxRetries)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Extraction aborted.");
                    // Re‑throw or handle as needed; here we exit the loop
                    break;
                }
                // Optionally, introduce a short delay before retrying
                // System.Threading.Thread.Sleep(500);
            }
        }
    }
}