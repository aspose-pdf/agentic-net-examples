using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Entry point
    static async Task Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputPdfPath = "output_with_attachment.pdf";
        const string fileUrl       = "https://example.com/sample.docx"; // remote file URL
        const string attachmentName = "sample.docx";       // name to show in the attachment

        // Verify source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Download the remote file into a memory stream
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

        // Open the PDF, create the attachment annotation, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation icon (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a FileSpecification from the in‑memory bytes
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                FileSpecification fileSpec = new FileSpecification(ms, attachmentName);

                // Create the file attachment annotation
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Optional visual settings (Icon property removed due to API version differences)
                    Color = Aspose.Pdf.Color.Blue,
                    Contents = $"Attached file: {attachmentName}"
                };

                // Add the annotation to the page
                page.Annotations.Add(attachment);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdfPath}");
    }
}
