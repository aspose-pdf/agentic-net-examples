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
        const string fileUrl       = "https://example.com/sample.pdf"; // remote file URL
        const string attachmentName = "sample.pdf";        // name shown in the attachment

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Download the remote file into memory
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

        // Open the PDF, add the attachment, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create a FileSpecification from the in‑memory bytes
            // The constructor accepts a stream and a display name
            FileSpecification fileSpec = new FileSpecification(new MemoryStream(fileBytes), attachmentName);

            // Create the file attachment annotation
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional visual and descriptive settings
                // Icon property removed because the enum name changed in newer versions; default icon (Paperclip) will be used.
                Title    = "Remote File Attachment",
                Contents = $"Attached file from {fileUrl}",
                Color    = Aspose.Pdf.Color.Blue,                     // border color
                Opacity  = 0.9f
            };

            // Add the annotation to the page
            page.Annotations.Add(attachment);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdfPath}");
    }
}
