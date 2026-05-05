using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachment.pdf";
        const string attachmentUrl = "https://example.com/file.pdf";
        const string attachmentName = "file.pdf";
        const string description = "Network attachment";

        // Retry configuration
        const int maxRetries = 3;
        const int delayMs = 2000; // 2 seconds between attempts

        // Verify source PDF exists before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF '{inputPdf}' not found. Aborting.");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Attempt to download the attachment with retry logic
        Stream? attachmentStream = null; // nullable to satisfy nullable reference analysis
        int attempt = 0;
        bool downloaded = false;

        using (HttpClient client = new HttpClient())
        {
            client.Timeout = TimeSpan.FromSeconds(10); // request timeout

            while (attempt < maxRetries && !downloaded)
            {
                try
                {
                    attempt++;
                    // GetStreamAsync returns a stream that reads directly from the network
                    attachmentStream = await client.GetStreamAsync(attachmentUrl);
                    downloaded = true; // success
                }
                catch (TaskCanceledException ex) // timeout or cancellation
                {
                    Console.Error.WriteLine($"Attempt {attempt} timed out: {ex.Message}");
                    if (attempt < maxRetries)
                        await Task.Delay(delayMs);
                }
                catch (Exception ex) // other errors – abort retries
                {
                    Console.Error.WriteLine($"Attempt {attempt} failed: {ex.Message}");
                    break;
                }
            }
        }

        if (!downloaded || attachmentStream == null)
        {
            Console.Error.WriteLine("Failed to download attachment after retries.");
            return;
        }

        // Add the attachment to the PDF
        using (attachmentStream) // ensure the network stream is disposed
        {
            editor.AddDocumentAttachment(attachmentStream, attachmentName, description);
        }

        // Save the modified PDF
        editor.Save(outputPdf);
        Console.WriteLine($"PDF saved with attachment to '{outputPdf}'.");
    }
}
