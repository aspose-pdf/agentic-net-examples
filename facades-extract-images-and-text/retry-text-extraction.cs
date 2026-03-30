using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
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
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(inputPath);
                    extractor.ExtractText();
                    extractor.GetText(outputPath);
                }

                success = true;
                Console.WriteLine($"Text extracted successfully on attempt {attempt}.");
            }
            catch (IOException ioEx)
            {
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
        }
    }
}
