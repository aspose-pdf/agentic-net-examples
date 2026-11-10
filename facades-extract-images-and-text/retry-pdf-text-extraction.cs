using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Retry up to three times if an IOException occurs during extraction
        const int maxAttempts = 3;
        int attempt = 0;
        bool success = false;

        while (attempt < maxAttempts && !success)
        {
            attempt++;
            try
            {
                // PdfExtractor implements IDisposable – wrap in using for deterministic disposal
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Load the PDF document
                    extractor.BindPdf(inputPath);

                    // Enable text extraction – call the method, not a property
                    extractor.ExtractText();

                    // Extract text to a memory stream
                    using (MemoryStream textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream);
                        textStream.Position = 0;
                        using (StreamReader reader = new StreamReader(textStream))
                        {
                            string extractedText = reader.ReadToEnd();
                            File.WriteAllText(outputPath, extractedText);
                        }
                    }
                }

                // If we reach this point, extraction succeeded
                success = true;
                Console.WriteLine($"Extraction succeeded on attempt {attempt}. Output saved to '{outputPath}'.");
            }
            catch (IOException ioEx)
            {
                // Log the IOException and retry if attempts remain
                Console.Error.WriteLine($"IOException on attempt {attempt}: {ioEx.Message}");
                if (attempt >= maxAttempts)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Extraction failed.");
                }
                else
                {
                    // Optional: brief pause before retry
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                // Non-IO exceptions are not retried
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }
    }
}
