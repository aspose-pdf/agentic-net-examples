using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static async Task Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachment.pdf";
        const string fileUrl = "https://example.com/sample.txt";
        const string attachmentFileName = "sample.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Download the remote file into a byte array
        byte[] fileBytes;
        using (HttpClient http = new HttpClient())
        {
            try
            {
                fileBytes = await http.GetByteArrayAsync(fileUrl);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to download file: {ex.Message}");
                return;
            }
        }

        // Open the PDF, add a file attachment annotation, and save
        using (Document doc = new Document(inputPdf))
        {
            // Use the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create a FileSpecification from the in‑memory stream
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                // The constructor expects a file name and a description; the actual content is set via the Contents property.
                FileSpecification fileSpec = new FileSpecification(attachmentFileName, attachmentFileName);
                fileSpec.Contents = ms;

                // Create the FileAttachmentAnnotation
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Icon property is optional; the enum FileAttachmentAnnotationIcon does not exist in the current Aspose.Pdf version.
                    // If a visual icon is desired, a string value can be assigned, e.g., "Paperclip".
                    // Icon = "Paperclip",
                    Contents = $"Attached file: {attachmentFileName}",
                    Title = "Remote File"
                };

                // Add the annotation to the page
                page.Annotations.Add(attachment);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdf}");
    }
}
