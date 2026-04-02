using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const int maxRetries = 3;
        const int delayMilliseconds = 1000;
        int attempt = 0;
        bool success = false;
        const string outputPath = "output.pdf";

        while (attempt < maxRetries && !success)
        {
            try
            {
                using (Document document = new Document())
                {
                    Page page = document.Pages.Add();
                    TextFragment fragment = new TextFragment("Hello, Aspose.Pdf!");
                    page.Paragraphs.Add(fragment);

                    // Save the document with platform‑aware handling.
                    SaveDocument(document, outputPath);
                }

                success = true;
                Console.WriteLine("PDF generated successfully.");
            }
            catch (Exception ex) when (IsTransient(ex))
            {
                attempt++;
                Console.WriteLine($"Attempt {attempt} failed (transient): {ex.Message}");
                if (attempt >= maxRetries)
                {
                    Console.WriteLine("All retry attempts exhausted.");
                    throw;
                }
                Thread.Sleep(delayMilliseconds);
            }
            catch (Exception ex)
            {
                // Non‑transient errors (e.g., missing GDI+ on non‑Windows) are reported and the loop exits.
                Console.WriteLine($"Fatal error: {ex.Message}");
                throw;
            }
        }
    }

    private static void SaveDocument(Document doc, string path)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // Windows has GDI+ built‑in – safe to call Save directly.
            doc.Save(path);
        }
        else
        {
            // On macOS / Linux the native GDI+ library (libgdiplus) may be missing.
            // Attempt to save and gracefully handle the missing library.
            try
            {
                doc.Save(path);
                Console.WriteLine($"PDF saved to '{path}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                  "The PDF was generated in memory but cannot be saved to disk.");
                // Optionally, you could write the document to a MemoryStream here.
            }
        }
    }

    // Determines whether an exception is considered transient (eligible for a retry).
    private static bool IsTransient(Exception ex)
    {
        // I/O errors (e.g., file locked, network share unavailable) are transient.
        if (ex is IOException)
            return true;

        // Aspose‑PDF specific errors that are usually transient.
        if (ex is PdfException)
            return true;

        // TypeInitializationException that wraps a DllNotFoundException is NOT transient – handled elsewhere.
        return false;
    }

    // Walks the inner‑exception chain to detect a missing native library.
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
