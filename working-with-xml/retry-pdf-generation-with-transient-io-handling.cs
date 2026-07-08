using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;

class PdfGenerator
{
    // Maximum number of retry attempts for transient failures
    private const int MaxRetries = 3;
    // Base delay in milliseconds for exponential back‑off
    private const int BaseDelayMs = 500;

    /// <summary>
    /// Generates a PDF by loading an existing file, performing a simple modification,
    /// and saving the result. Transient I/O errors are handled with retry logic.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF.</param>
    /// <param name="outputPath">Path where the generated PDF will be saved.</param>
    public static void GeneratePdf(string inputPath, string outputPath)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        int attempt = 0;
        while (true)
        {
            try
            {
                // === Load the document (using rule: document-disposal-with-using) ===
                using (Document doc = new Document(inputPath))
                {
                    // Example modification: add a blank page (demonstrates PDF generation)
                    doc.Pages.Add();

                    // === Save the document (PDF format) ===
                    // Save(string) without SaveOptions always writes PDF.
                    doc.Save(outputPath);
                }

                Console.WriteLine($"PDF generated successfully: {outputPath}");
                break; // Success – exit retry loop
            }
            catch (PdfException ex) // Catch Aspose.Pdf specific errors
            {
                attempt++;
                if (attempt > MaxRetries)
                {
                    Console.Error.WriteLine($"Failed after {MaxRetries} attempts: {ex.Message}");
                    throw; // Re‑throw after exhausting retries
                }

                // Transient error – wait before retrying (exponential back‑off)
                int delay = BaseDelayMs * (int)Math.Pow(2, attempt - 1);
                Console.Error.WriteLine($"Transient PDF error ({ex.Message}), retry {attempt}/{MaxRetries} after {delay}ms...");
                Thread.Sleep(delay);
            }
            catch (IOException ex) // Catch generic I/O errors (e.g., file lock)
            {
                attempt++;
                if (attempt > MaxRetries)
                {
                    Console.Error.WriteLine($"IO failure after {MaxRetries} attempts: {ex.Message}");
                    throw;
                }

                int delay = BaseDelayMs * (int)Math.Pow(2, attempt - 1);
                Console.Error.WriteLine($"IO error ({ex.Message}), retry {attempt}/{MaxRetries} after {delay}ms...");
                Thread.Sleep(delay);
            }
        }
    }

    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string outputPdf = "generated.pdf";

        GeneratePdf(inputPdf, outputPdf);
    }
}