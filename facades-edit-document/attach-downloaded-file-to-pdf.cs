using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        // Paths for the source PDF and the output PDF
        const string sourcePdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // URL of the file to download and attach (replace with a valid URL)
        const string fileUrl = "https://example.com/file.bin";

        // Name and description for the attachment inside the PDF
        const string attachmentName = "file.bin";
        const string attachmentDescription = "File downloaded from network";

        // Ensure the source PDF exists – create an empty one if it does not.
        if (!File.Exists(sourcePdfPath))
        {
            using var emptyDoc = new Document();
            emptyDoc.Pages.Add(); // add a blank page so the PDF is valid
            emptyDoc.Save(sourcePdfPath);
            Console.WriteLine($"Source PDF not found. Created empty placeholder at '{sourcePdfPath}'.");
        }

        // Download the file into a byte array – handle possible HTTP errors gracefully
        byte[]? fileBytes = null;
        try
        {
            using var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(fileUrl);
            response.EnsureSuccessStatusCode(); // throws if status is not 2xx
            fileBytes = await response.Content.ReadAsByteArrayAsync();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Failed to download attachment from '{fileUrl}'. Reason: {ex.Message}");
            Console.WriteLine("The PDF will be saved without the attachment.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while downloading the file: {ex.Message}");
            Console.WriteLine("The PDF will be saved without the attachment.");
        }

        // Initialize the PdfContentEditor facade and bind the source PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(sourcePdfPath);

            // Add the attachment only if we successfully retrieved the bytes
            if (fileBytes != null && fileBytes.Length > 0)
            {
                using var attachmentStream = new MemoryStream(fileBytes);
                editor.AddDocumentAttachment(attachmentStream, attachmentName, attachmentDescription);
                Console.WriteLine($"Attachment '{attachmentName}' added to the PDF.");
            }
            else
            {
                Console.WriteLine("No attachment was added because the download failed or returned empty content.");
            }

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF processing completed. Output file: '{outputPdfPath}'.");
    }
}
