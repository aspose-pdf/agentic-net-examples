using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Async Main to allow awaiting the download
    static async Task Main()
    {
        const string inputPdfPath  = "input.pdf";          // Existing PDF to modify
        const string outputPdfPath = "output.pdf";         // Resulting PDF with attachment
        const string fileUrl       = "https://example.com/file.bin"; // URL of the file to attach
        const string attachmentName = "file.bin";          // Name that will appear in the PDF
        const string description    = "Downloaded attachment"; // Description for the attachment

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Download the file into a byte array
        byte[] fileBytes;
        using (HttpClient httpClient = new HttpClient())
        {
            fileBytes = await httpClient.GetByteArrayAsync(fileUrl);
        }

        // Wrap the byte array in a MemoryStream for Aspose.Pdf.Facades
        using (MemoryStream attachmentStream = new MemoryStream(fileBytes))
        // PdfContentEditor implements IDisposable, so use a using block
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdfPath);

            // Add the attachment without a visible annotation
            editor.AddDocumentAttachment(attachmentStream, attachmentName, description);

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"File attached and saved to '{outputPdfPath}'.");
    }
}