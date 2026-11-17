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
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "output.pdf";        // result PDF
        const string fileUrl = "https://example.com/file.txt"; // remote file to attach

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        // Download remote file into memory
        byte[] fileBytes;
        using (HttpClient client = new HttpClient())
        {
            try
            {
                fileBytes = await client.GetByteArrayAsync(fileUrl);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to download file: {ex.Message}");
                return;
            }
        }

        // Open PDF and attach the downloaded file
        using (Document doc = new Document(inputPath))
        {
            // Ensure the PDF has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("PDF contains no pages.");
                return;
            }

            // Define annotation rectangle (position on the page)
            // Rectangle(left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 150, 750);

            // Create a FileSpecification from the in‑memory bytes
            string fileName = Path.GetFileName(fileUrl);
            var fileSpec = new FileSpecification(fileName)
            {
                Description = "File downloaded from remote URL",
                Contents = new MemoryStream(fileBytes)
            };

            // Create file attachment annotation on the first page, passing the FileSpecification
            var attachment = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec)
            {
                Icon = FileIcon.PushPin,          // visual icon
                Title = "Attached File",
                Contents = "File downloaded from remote URL"
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(attachment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Remote file attached and PDF saved to '{outputPath}'.");
    }
}
