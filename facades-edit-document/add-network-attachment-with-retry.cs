using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    // Maximum number of retry attempts when a network timeout occurs
    private const int MaxRetryAttempts = 3;

    // Timeout for each HTTP request (in seconds)
    private const int HttpTimeoutSeconds = 10;

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // PDF to which the attachment will be added
        const string outputPdfPath = "output_with_attachment.pdf";
        const string attachmentUrl = "https://example.com/file_to_attach.pdf";
        const string attachmentName = "file_to_attach.pdf"; // Name that will appear in the PDF
        const string annotationContents = "Attached file from network";
        const int targetPage = 1; // 1‑based page index
        const string iconName = "Graph"; // Valid values: Graph, PushPin, Paperclip, Tag

        // Ensure the source PDF exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Create the PdfContentEditor facade
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdfPath);

            // Attempt to download the attachment stream with retry logic
            Stream attachmentStream = null;
            int attempt = 0;
            bool downloaded = false;

            while (attempt < MaxRetryAttempts && !downloaded)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.Timeout = TimeSpan.FromSeconds(HttpTimeoutSeconds);
                        // Synchronous call for simplicity; in production async/await is preferred
                        Task<Stream> downloadTask = client.GetStreamAsync(attachmentUrl);
                        attachmentStream = downloadTask.GetAwaiter().GetResult();

                        // If we reach this point, the download succeeded
                        downloaded = true;
                    }
                }
                catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
                {
                    // This exception is thrown when the request times out
                    attempt++;
                    Console.WriteLine($"Timeout while downloading attachment (attempt {attempt}/{MaxRetryAttempts}). Retrying...");
                    if (attempt >= MaxRetryAttempts)
                    {
                        Console.Error.WriteLine("Maximum retry attempts reached. Unable to download attachment.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // Any other exception is considered fatal for this operation
                    Console.Error.WriteLine($"Error downloading attachment: {ex.Message}");
                    return;
                }
            }

            // At this point we have a valid attachment stream
            using (attachmentStream) // Ensure the network stream is disposed after use
            {
                // Create a file attachment annotation on the specified page
                // Rectangle is defined in points (1/72 inch). Adjust as needed.
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 100, 100);

                editor.CreateFileAttachment(
                    rect,
                    annotationContents,
                    attachmentStream,
                    attachmentName,
                    targetPage,
                    iconName);

                // Save the modified PDF
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
        }
    }
}