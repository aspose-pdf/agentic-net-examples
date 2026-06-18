using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfGenerationWithRetry
{
    // Configuration for retry logic
    private const int MaxRetryAttempts = 3;          // maximum number of attempts
    private const int RetryDelayMilliseconds = 1000; // wait time between attempts

    static void Main()
    {
        const string outputPath = "generated.pdf";

        // Ensure the output directory exists (handle possible null from GetDirectoryName)
        string? outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (string.IsNullOrEmpty(outputDir))
        {
            outputDir = Directory.GetCurrentDirectory();
        }
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Attempt PDF generation with retry on transient I/O errors (PdfException)
        for (int attempt = 1; attempt <= MaxRetryAttempts; attempt++)
        {
            try
            {
                // === CREATE DOCUMENT (lifecycle rule) ===
                using (Document doc = new Document())
                {
                    // Add a simple page with some text
                    Page page = doc.Pages.Add();

                    // Create a text fragment
                    TextFragment tf = new TextFragment("Hello, Aspose.Pdf with retry logic!");
                    tf.Position = new Position(100, 700);
                    tf.TextState.FontSize = 14;
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

                    // Add the text fragment to the page
                    page.Paragraphs.Add(tf);

                    // === SAVE DOCUMENT (lifecycle rule) ===
                    // Guard Document.Save on non‑Windows platforms where libgdiplus may be missing
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        doc.Save(outputPath);
                    }
                    else
                    {
                        try
                        {
                            doc.Save(outputPath);
                        }
                        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                        {
                            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI+ dependent features.");
                        }
                    }
                }

                // If we reach this point, generation succeeded; exit the retry loop
                Console.WriteLine($"PDF generated successfully at '{outputPath}'.");
                break;
            }
            catch (PdfException ex) // Transient I/O related PDF errors
            {
                Console.Error.WriteLine($"Attempt {attempt} failed: {ex.Message}");

                if (attempt == MaxRetryAttempts)
                {
                    // All retries exhausted – rethrow to signal failure
                    throw;
                }

                // Wait before the next retry
                Thread.Sleep(RetryDelayMilliseconds);
            }
            catch (IOException ex) // General I/O errors (e.g., file lock)
            {
                Console.Error.WriteLine($"Attempt {attempt} encountered I/O error: {ex.Message}");

                if (attempt == MaxRetryAttempts)
                {
                    throw;
                }

                Thread.Sleep(RetryDelayMilliseconds);
            }
        }
    }

    // Helper to detect a missing native GDI+ library (libgdiplus) wrapped in TypeInitializationException
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
