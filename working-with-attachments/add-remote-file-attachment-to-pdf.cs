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
        const string outputPdfPath = "output_with_attachment.pdf"; // result PDF
        const string fileUrl       = "https://example.com/sample.txt"; // remote file URL

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Download the remote file into memory
        byte[] fileBytes;
        string fileName;
        using (HttpClient http = new HttpClient())
        {
            try
            {
                fileBytes = await http.GetByteArrayAsync(fileUrl);
                // Extract file name from URL
                fileName = Path.GetFileName(new Uri(fileUrl).AbsolutePath);
                if (string.IsNullOrEmpty(fileName))
                    fileName = "attachment.bin";
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to download file: {ex.Message}");
                return;
            }
        }

        // Open the PDF, add the attachment annotation, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the annotation icon
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a FileSpecification from the in‑memory file (note the argument order)
            FileSpecification fileSpec = new FileSpecification(new MemoryStream(fileBytes), fileName);

            // Create the FileAttachment annotation
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional visual and descriptive settings
                // Icon property is optional; remove IconType usage for compatibility with newer SDK versions
                Title    = "Remote File Attachment",
                Contents = $"Attached file: {fileName}",
                Color    = Aspose.Pdf.Color.LightGray
            };

            // Add the annotation to the page
            page.Annotations.Add(attachment);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdfPath}");
    }
}
