using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentUrl = "https://example.com/file.pdf";
        const int maxRetryAttempts = 3;
        const int retryDelayMilliseconds = 2000;

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Use PdfContentEditor facade to modify the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPdfPath);

            Stream attachmentStream = null;

            // Retry logic for downloading the attachment stream
            for (int attempt = 1; attempt <= maxRetryAttempts; attempt++)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        // Set a per‑request timeout (e.g., 10 seconds)
                        client.Timeout = TimeSpan.FromSeconds(10);
                        // Synchronously get the stream (blocking call)
                        attachmentStream = client.GetStreamAsync(attachmentUrl).GetAwaiter().GetResult();
                    }

                    // Successfully obtained the stream; exit the retry loop
                    break;
                }
                catch (Exception ex) when (ex is TaskCanceledException || ex is HttpRequestException)
                {
                    // Handle timeout or network errors
                    Console.Error.WriteLine($"Attempt {attempt} failed: {ex.Message}");
                    if (attempt == maxRetryAttempts)
                    {
                        Console.Error.WriteLine("All retry attempts exhausted. Attachment will not be added.");
                        attachmentStream = null;
                    }
                    else
                    {
                        // Wait before the next retry
                        Thread.Sleep(retryDelayMilliseconds);
                    }
                }
            }

            if (attachmentStream != null)
            {
                // Ensure the stream is at the beginning
                if (attachmentStream.CanSeek)
                    attachmentStream.Position = 0;

                // Add the attachment without a visible annotation
                editor.AddDocumentAttachment(attachmentStream, "attachment.pdf", "Network attachment");

                // Save the modified PDF
                editor.Save(outputPdfPath);
                Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");

                // Clean up the attachment stream
                attachmentStream.Dispose();
            }
        }
    }
}