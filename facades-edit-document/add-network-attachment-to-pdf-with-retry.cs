using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point – async to allow awaiting network calls
    static async Task Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "output.pdf";         // result PDF
        const string attachmentUrl  = "https://example.com/file.bin"; // network file
        const string attachmentName = "file.bin";           // name stored in PDF
        const string description    = "Network attachment"; // optional description

        // Verify source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Obtain the network stream with retry logic
        using (Stream attachmentStream = await GetStreamWithRetryAsync(
                   attachmentUrl, maxRetries: 3, delayMs: 2000))
        {
            if (attachmentStream == null)
            {
                Console.Error.WriteLine("Failed to download attachment after retries.");
                return;
            }

            // Load the PDF and add the attachment using Aspose.Pdf.Facades
            using (Document pdfDoc = new Document(inputPdfPath)) // lifecycle rule: using
            {
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(pdfDoc); // bind the in‑memory document

                // Add the attachment (no visual annotation) from the stream
                editor.AddDocumentAttachment(attachmentStream, attachmentName, description);

                // Save the modified PDF (lifecycle rule: using, then Save)
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
        }
    }

    // Helper: download a stream with simple retry on timeout
    static async Task<Stream> GetStreamWithRetryAsync(string url, int maxRetries, int delayMs)
    {
        using HttpClient httpClient = new HttpClient();

        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                // Per‑request timeout (10 seconds)
                httpClient.Timeout = TimeSpan.FromSeconds(10);
                // HttpClient returns a stream that the caller must dispose
                return await httpClient.GetStreamAsync(url);
            }
            catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
            {
                // Timeout occurred
                Console.Error.WriteLine($"Attempt {attempt}: request timed out.");
            }
            catch (Exception ex)
            {
                // Non‑timeout error – abort retries
                Console.Error.WriteLine($"Attempt {attempt}: {ex.Message}");
                break;
            }

            // Wait before next retry (if any attempts remain)
            if (attempt < maxRetries)
                await Task.Delay(delayMs);
        }

        // All attempts failed
        return null;
    }
}