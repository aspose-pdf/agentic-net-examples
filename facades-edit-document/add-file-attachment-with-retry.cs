using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Adds a file attachment to a PDF using a network stream.
    // If the download times out, the operation is retried up to maxRetries times.
    static async Task AddAttachmentWithRetryAsync(
        string pdfPath,
        string attachmentUrl,
        string outputPath,
        int maxRetries = 3)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // HttpClient is intended to be reused; however for this isolated example we dispose it after use.
        using (var httpClient = new HttpClient())
        {
            // Optional: set a per‑request timeout (e.g., 10 seconds)
            httpClient.Timeout = TimeSpan.FromSeconds(10);

            int attempt = 0;
            bool success = false;

            while (attempt < maxRetries && !success)
            {
                attempt++;
                try
                {
                    // Download the attachment as a stream.
                    using (Stream networkStream = await httpClient.GetStreamAsync(attachmentUrl))
                    {
                        // Use PdfContentEditor to add the attachment.
                        using (var editor = new Aspose.Pdf.Facades.PdfContentEditor())
                        {
                            editor.BindPdf(pdfPath);

                            // System.Drawing.Rectangle is required by the API.
                            var rect = new System.Drawing.Rectangle(0, 0, 100, 100);

                            // The attachment name is derived from the URL.
                            string attachmentName = Path.GetFileName(new Uri(attachmentUrl).LocalPath);

                            // Create the file attachment annotation using the network stream.
                            editor.CreateFileAttachment(
                                rect,
                                "Attachment added via network stream",
                                networkStream,
                                attachmentName,
                                1,                 // page number (1‑based)
                                "Paperclip");      // icon name

                            // Save the modified PDF.
                            editor.Save(outputPath);
                        }
                    }

                    success = true; // If we reach here, the operation succeeded.
                }
                catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
                {
                    // HttpClient throws TaskCanceledException on timeout.
                    Console.Error.WriteLine($"Attempt {attempt} timed out. Retrying...");
                    if (attempt >= maxRetries)
                    {
                        Console.Error.WriteLine("Maximum retry attempts reached. Operation failed.");
                    }
                }
                catch (Exception ex)
                {
                    // Any other exception is considered fatal for the retry loop.
                    Console.Error.WriteLine($"Error on attempt {attempt}: {ex.Message}");
                    break;
                }
            }
        }
    }

    static async Task Main(string[] args)
    {
        // Example usage:
        // args[0] = input PDF path
        // args[1] = attachment URL
        // args[2] = output PDF path
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputPdf> <attachmentUrl> <outputPdf>");
            return;
        }

        string inputPdf = args[0];
        string attachmentUrl = args[1];
        string outputPdf = args[2];

        await AddAttachmentWithRetryAsync(inputPdf, attachmentUrl, outputPdf);
        Console.WriteLine("Process completed.");
    }
}