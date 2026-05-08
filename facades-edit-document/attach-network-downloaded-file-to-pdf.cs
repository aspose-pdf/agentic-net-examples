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
        // Paths for the output PDF (the source PDF will be created in‑memory)
        const string outputPdfPath = "output.pdf";

        // URL of the file to be attached (downloaded as a byte array)
        const string fileUrl = "https://example.com/file.bin";

        // Name and description for the attachment inside the PDF
        const string attachmentName = "file.bin";
        const string attachmentDescription = "Network downloaded attachment";

        // ------------------------------------------------------------
        // 1. Download the file into a byte array – handle possible HTTP errors gracefully
        // ------------------------------------------------------------
        byte[] fileBytes;
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                fileBytes = await httpClient.GetByteArrayAsync(fileUrl);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Failed to download '{fileUrl}': {ex.Message}");
                // Fallback: use an empty byte array so the PDF can still be saved
                fileBytes = Array.Empty<byte>();
            }
        }

        // ------------------------------------------------------------
        // 2. Create a minimal PDF in memory (so we don't depend on an external file)
        // ------------------------------------------------------------
        using (MemoryStream sourcePdfStream = new MemoryStream())
        {
            // Create a one‑page blank PDF
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // adds a blank page
                doc.Save(sourcePdfStream);
            }

            // Reset the stream position before reading it again
            sourcePdfStream.Position = 0;

            // ------------------------------------------------------------
            // 3. Create a memory stream from the downloaded (or fallback) bytes
            // ------------------------------------------------------------
            using (MemoryStream attachmentStream = new MemoryStream(fileBytes))
            {
                // ------------------------------------------------------------
                // 4. Bind the in‑memory PDF, add the attachment, and save the result
                // ------------------------------------------------------------
                PdfContentEditor editor = new PdfContentEditor();
                // Use the overload that accepts a Stream to avoid file‑system dependencies
                editor.BindPdf(sourcePdfStream);
                editor.AddDocumentAttachment(attachmentStream, attachmentName, attachmentDescription);
                editor.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}
