using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        // Paths for the source PDF and the output PDF
        const string sourcePdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // URL of the file to be attached
        const string attachmentUrl = "https://example.com/file.bin";

        // Name and description for the attachment
        const string attachmentName = "file.bin";
        const string attachmentDescription = "Network downloaded attachment";

        // Download the file into a byte array with proper HTTP status handling
        byte[] attachmentBytes;
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = await httpClient.GetAsync(attachmentUrl);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to download attachment. Status: {(int)response.StatusCode} {response.ReasonPhrase}");
                return; // Exit gracefully instead of throwing an exception
            }
            attachmentBytes = await response.Content.ReadAsByteArrayAsync();
        }

        // Create a memory stream from the downloaded bytes and attach it to the PDF
        using (MemoryStream attachmentStream = new MemoryStream(attachmentBytes))
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(sourcePdfPath);
            editor.AddDocumentAttachment(attachmentStream, attachmentName, attachmentDescription);
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}