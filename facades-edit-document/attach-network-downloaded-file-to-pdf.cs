using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main(string[] args)
    {
        // Paths for the source PDF and the output PDF
        const string sourcePdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // URL of the file to download and attach
        const string attachmentUrl = "https://example.com/file.bin"; // replace with a valid URL
        const string attachmentName = "file.bin"; // name that will appear in the PDF
        const string attachmentDescription = "Network downloaded attachment";

        // ------------------------------------------------------------
        // Ensure a source PDF exists – create a minimal one if missing.
        // ------------------------------------------------------------
        if (!File.Exists(sourcePdfPath))
        {
            var blankDoc = new Document();
            blankDoc.Pages.Add(); // add a single empty page
            blankDoc.Save(sourcePdfPath);
            Console.WriteLine($"Source PDF not found. Created a new blank PDF at '{sourcePdfPath}'.");
        }

        // ------------------------------------------------------------
        // Download the attachment into a byte array – handle HTTP errors.
        // ------------------------------------------------------------
        byte[]? attachmentBytes = null;
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                attachmentBytes = await httpClient.GetByteArrayAsync(attachmentUrl);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Failed to download attachment from '{attachmentUrl}'. Error: {ex.Message}");
                Console.WriteLine("The PDF will be saved without the attachment.");
                // attachmentBytes stays null to indicate no attachment.
            }
        }

        // ------------------------------------------------------------
        // Load the existing PDF and optionally add the attachment.
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(sourcePdfPath);

            if (attachmentBytes != null && attachmentBytes.Length > 0)
            {
                // Use a MemoryStream overload – no annotation is added.
                using (MemoryStream attachmentStream = new MemoryStream(attachmentBytes))
                {
                    editor.AddDocumentAttachment(attachmentStream, attachmentName, attachmentDescription);
                }
            }

            // Save the modified PDF.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'." +
                          (attachmentBytes != null ? " Attachment added." : " No attachment added."));
    }
}
