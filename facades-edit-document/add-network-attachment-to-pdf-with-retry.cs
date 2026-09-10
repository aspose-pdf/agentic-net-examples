using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    // Maximum number of retry attempts when downloading the attachment
    private const int MaxRetryAttempts = 3;
    // Delay between retries (in milliseconds)
    private const int RetryDelayMs = 2000;

    static async Task Main()
    {
        const string inputPdfPath  = "input.pdf";          // PDF to which the attachment will be added
        const string outputPdfPath = "output_with_attachment.pdf";
        const string attachmentUrl = "https://example.com/attachment_file.pdf"; // Network URL of the file to attach

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Download the attachment stream with retry logic
        Stream attachmentStream = await DownloadWithRetryAsync(attachmentUrl, MaxRetryAttempts);
        if (attachmentStream == null)
        {
            Console.Error.WriteLine("Failed to download attachment after retries.");
            return;
        }

        // Use PdfContentEditor to add the attachment annotation
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file
            editor.BindPdf(inputPdfPath);

            // Define the rectangle where the attachment icon will appear (System.Drawing.Rectangle)
            var rect = new System.Drawing.Rectangle(0, 0, 100, 100);

            // Add the file attachment annotation using the downloaded stream
            // Parameters: rectangle, contents, stream, attachment name, page number (1‑based), icon name
            editor.CreateFileAttachment(rect, "Network attachment", attachmentStream, "attachment_file.pdf", 1, "Paperclip");

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        // Dispose the downloaded stream
        attachmentStream.Dispose();

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }

    // Downloads a stream from the given URL with retry on timeout-related exceptions
    private static async Task<Stream> DownloadWithRetryAsync(string url, int maxAttempts)
    {
        using (HttpClient client = new HttpClient())
        {
            // Optional: set a per‑request timeout
            client.Timeout = TimeSpan.FromSeconds(10);

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    // Get the response stream
                    HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();

                    // Return a copy of the response stream that can be read multiple times if needed
                    MemoryStream memory = new MemoryStream();
                    await response.Content.CopyToAsync(memory);
                    memory.Position = 0;
                    return memory;
                }
                catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
                {
                    // Timeout occurred
                    Console.Error.WriteLine($"Attempt {attempt}: Request timed out. Retrying...");
                }
                catch (HttpRequestException ex)
                {
                    // Network error (could be timeout as well)
                    Console.Error.WriteLine($"Attempt {attempt}: Network error - {ex.Message}. Retrying...");
                }

                if (attempt < maxAttempts)
                {
                    await Task.Delay(RetryDelayMs);
                }
            }
        }

        // All attempts failed
        return null;
    }
}