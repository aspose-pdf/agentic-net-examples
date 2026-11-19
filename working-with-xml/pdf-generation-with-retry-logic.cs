using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int maxRetries = 3;
        const int delayMs = 2000;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Example modification: add a blank page
                doc.Pages.Add();

                // Save with retry logic
                SaveWithRetry(doc, outputPath, maxRetries, delayMs);
            }

            Console.WriteLine($"PDF generated successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to generate PDF: {ex.Message}");
        }
    }

    static void SaveWithRetry(Document doc, string path, int maxAttempts, int delayMilliseconds)
    {
        int attempt = 0;
        while (true)
        {
            try
            {
                // Save PDF (no SaveOptions needed for PDF format)
                doc.Save(path);
                break; // success
            }
            catch (IOException ioEx)
            {
                attempt++;
                if (attempt >= maxAttempts)
                    throw; // rethrow after max attempts

                Console.Error.WriteLine($"I/O error on attempt {attempt}: {ioEx.Message}. Retrying in {delayMilliseconds}ms...");
                Thread.Sleep(delayMilliseconds);
            }
        }
    }
}