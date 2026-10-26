using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Maximum number of retry attempts for saving a PDF
    private const int MaxSaveAttempts = 3;
    // Base delay in milliseconds between retries (exponential back‑off)
    private const int BaseDelayMs = 500;

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "generated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block (lifecycle rule)
            using (Document doc = new Document(inputPath))
            {
                // Perform any PDF generation/modification here
                // For demonstration, add a simple text fragment to the first page
                if (doc.Pages.Count > 0)
                {
                    Page page = doc.Pages[1]; // 1‑based indexing (global rule)
                    TextFragment tf = new TextFragment("Generated content");
                    page.Paragraphs.Add(tf);
                }

                // Save with retry logic
                SaveWithRetry(doc, outputPath);
            }

            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Fatal error: {ex.Message}");
        }
    }

    // Saves the document with retry handling for transient I/O errors
    private static void SaveWithRetry(Document doc, string path)
    {
        int attempt = 0;
        while (true)
        {
            try
            {
                // Save operation (lifecycle rule)
                doc.Save(path);
                // If save succeeds, exit the loop
                break;
            }
            catch (IOException ioEx)
            {
                attempt++;
                if (attempt >= MaxSaveAttempts)
                {
                    // Exhausted retries – rethrow the exception
                    throw new IOException($"Failed to save PDF after {MaxSaveAttempts} attempts.", ioEx);
                }

                // Calculate exponential back‑off delay
                int delay = BaseDelayMs * (int)Math.Pow(2, attempt - 1);
                Console.Error.WriteLine($"I/O error on save attempt {attempt}: {ioEx.Message}. Retrying in {delay} ms...");
                Thread.Sleep(delay);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                // Treat as transient (e.g., file locked) and retry similarly
                attempt++;
                if (attempt >= MaxSaveAttempts)
                {
                    throw new UnauthorizedAccessException($"Failed to save PDF after {MaxSaveAttempts} attempts.", uaEx);
                }

                int delay = BaseDelayMs * (int)Math.Pow(2, attempt - 1);
                Console.Error.WriteLine($"Access error on save attempt {attempt}: {uaEx.Message}. Retrying in {delay} ms...");
                Thread.Sleep(delay);
            }
        }
    }
}
