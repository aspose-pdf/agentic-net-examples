using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string fileUrl = "https://example.com/sample.docx"; // remote file URL
        const string attachmentName = "sample.docx"; // name to show in the PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Download the remote file into memory
        byte[] fileBytes;
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                fileBytes = httpClient.GetByteArrayAsync(fileUrl).Result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to download file: {ex.Message}");
                return;
            }
        }

        // Open the PDF, add the attachment, and save
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Choose the first page (or any other page) to host the annotation
            Page page = pdfDoc.Pages[1];

            // Create a rectangle where the attachment icon will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the attachment from the in‑memory stream
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                // Note: In the current Aspose.Pdf version the FileSpecification constructor expects
                // the stream as the first argument, followed by the name and description.
                FileSpecification fileSpec = new FileSpecification(ms, attachmentName, attachmentName);
                // Optional: set modification date metadata
                fileSpec.Params.ModDate = DateTime.UtcNow;

                // Add the file specification to the document's embedded files collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);

                // Create the file attachment annotation
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    Title = "Attached Document",
                    Contents = $"File: {attachmentName}"
                };

                // Add the annotation to the page
                page.Annotations.Add(attachment);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdfPath}");
    }
}
