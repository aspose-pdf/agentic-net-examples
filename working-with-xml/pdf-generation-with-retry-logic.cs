using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfGeneratorWithRetry
{
    // Path to the generated PDF file
    private const string OutputPath = "generated.pdf";

    // Maximum number of retry attempts for transient I/O errors
    private const int MaxRetryAttempts = 3;

    // Delay (in milliseconds) between retry attempts
    private const int RetryDelayMs = 2000;

    static void Main()
    {
        int attempt = 0;
        while (true)
        {
            attempt++;
            try
            {
                // === CREATE DOCUMENT (lifecycle rule: use using for disposal) ===
                using (Document doc = new Document())
                {
                    // Add a single page
                    Page page = doc.Pages.Add();

                    // Add a simple text fragment to the page
                    TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");
                    tf.Position = new Position(100, 700);
                    page.Paragraphs.Add(tf);

                    // === SAVE DOCUMENT (lifecycle rule) ===
                    // Save as PDF; no SaveOptions needed for PDF format
                    doc.Save(OutputPath);
                }

                // If we reach this point, generation succeeded; exit loop
                Console.WriteLine($"PDF generated successfully at '{Path.GetFullPath(OutputPath)}'.");
                break;
            }
            catch (PdfException ex) // Catch transient PDF/I/O errors
            {
                Console.Error.WriteLine($"Attempt {attempt} failed: {ex.Message}");

                if (attempt >= MaxRetryAttempts)
                {
                    // Re‑throw after exhausting retries
                    Console.Error.WriteLine("Maximum retry attempts reached. Operation aborted.");
                    throw;
                }

                // Wait before next retry
                Thread.Sleep(RetryDelayMs);
            }
            catch (IOException ex) // Additional catch for generic I/O issues
            {
                Console.Error.WriteLine($"Attempt {attempt} encountered I/O error: {ex.Message}");

                if (attempt >= MaxRetryAttempts)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Operation aborted.");
                    throw;
                }

                Thread.Sleep(RetryDelayMs);
            }
        }
    }
}