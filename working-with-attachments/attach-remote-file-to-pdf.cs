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
        const string fileUrl = "https://example.com/file.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Download the remote file into a byte array
        byte[] fileBytes;
        string fileName;
        using (HttpClient http = new HttpClient())
        {
            try
            {
                fileBytes = await http.GetByteArrayAsync(fileUrl);
                fileName = Path.GetFileName(new Uri(fileUrl).LocalPath);
                if (string.IsNullOrEmpty(fileName))
                    fileName = "attachment.bin";
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

            // Define the rectangle that will contain the annotation icon
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a FileSpecification from the in‑memory stream
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                FileSpecification fileSpec = new FileSpecification(ms, fileName);

                // Create the file attachment annotation
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // The Icon enum must be qualified with its declaring type (FileIcon)
                    Icon = FileIcon.Paperclip,
                    Title = "Remote File",
                    Contents = $"Attached file from {fileUrl}"
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
