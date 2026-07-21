using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;

public static class PdfGenerator
{
    // Generates a PDF using the supplied action with retry on transient I/O errors.
    public static void GeneratePdfWithRetry(string outputPath, Action<Document> buildPdf, int maxAttempts = 3, int delayMilliseconds = 1000)
    {
        if (string.IsNullOrEmpty(outputPath))
            throw new ArgumentException("outputPath is required", nameof(outputPath));
        if (buildPdf == null)
            throw new ArgumentNullException(nameof(buildPdf));

        int attempt = 0;
        while (true)
        {
            attempt++;
            try
            {
                // Create a new PDF document
                using (Document doc = new Document())
                {
                    // Let caller populate the document
                    buildPdf(doc);

                    // Save the document (PDF format)
                    doc.Save(outputPath);
                }

                // Success – exit the retry loop
                break;
            }
            catch (IOException ioEx) when (IsTransient(ioEx))
            {
                if (attempt >= maxAttempts)
                    throw; // rethrow after max attempts

                // Wait before retrying
                Thread.Sleep(delayMilliseconds);
            }
            catch (PdfException pdfEx) when (IsTransient(pdfEx))
            {
                if (attempt >= maxAttempts)
                    throw;

                Thread.Sleep(delayMilliseconds);
            }
        }
    }

    // Simple heuristic to decide if an exception is transient.
    private static bool IsTransient(Exception ex)
    {
        // For demonstration, treat all IO and PdfException as transient.
        // Real implementation could inspect HResult or inner exceptions.
        return true;
    }
}

// Example usage
class Program
{
    static void Main()
    {
        const string output = "generated.pdf";

        // Build a simple PDF with one page and a text fragment
        PdfGenerator.GeneratePdfWithRetry(output, doc =>
        {
            // Add a page
            var page = doc.Pages.Add();

            // Add text
            Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment("Hello, Aspose.Pdf!");
            page.Paragraphs.Add(tf);
        });
    }
}