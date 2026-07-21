using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Async Main to allow awaiting the download (C# 7.1+)
    static async Task Main()
    {
        const string inputPdfPath = "input.pdf";          // Existing PDF to modify
        const string outputPdfPath = "output.pdf";        // Resulting PDF with attachment
        const string fileUrl = "https://example.com/file.bin"; // URL of the file to attach
        const string attachmentName = "downloaded.bin";   // Name that will appear in the PDF
        const string description = "File downloaded from network";

        // ------------------------------------------------------------
        // Create a minimal PDF so the sandbox has a file to open.
        // ------------------------------------------------------------
        using (var seed = new Document())
        {
            seed.Pages.Add(); // add a blank page
            seed.Save(inputPdfPath);
        }

        byte[]? fileBytes = null; // nullable to silence CS8600 warning
        // Download the file into a byte array – handle non‑successful status codes gracefully
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(fileUrl);
                if (response.IsSuccessStatusCode)
                {
                    fileBytes = await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    Console.WriteLine($"Warning: Unable to download file. HTTP {(int)response.StatusCode} {response.ReasonPhrase}. Attachment will be skipped.");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error downloading file: {ex.Message}. Attachment will be skipped.");
            }
        }

        // Bind the source PDF document
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Add the attachment only if we actually have data
            if (fileBytes != null && fileBytes.Length > 0)
            {
                using (MemoryStream attachmentStream = new MemoryStream(fileBytes))
                {
                    editor.AddDocumentAttachment(attachmentStream, attachmentName, description);
                }
            }
            else
            {
                Console.WriteLine("No attachment added because the download failed or returned empty content.");
            }

            // Save the modified PDF (or the original PDF if no attachment was added)
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Process completed. Result saved to '{outputPdfPath}'.");
    }
}
