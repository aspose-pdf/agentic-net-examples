using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "customAttachment.txt";

        // Ensure an input PDF exists – create a minimal one if missing
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Example attachment data; replace with actual content as needed
        byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("This is the content of the attachment.");

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a FileSpecification from the memory stream and add it as an embedded file
            using (var ms = new MemoryStream(attachmentData))
            {
                var fileSpec = new FileSpecification(ms, attachmentName, "Embedded attachment")
                {
                    // Optional: set MIME type for the attachment
                    MIMEType = "text/plain"
                };
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with attachment to '{outputPath}'.");
    }
}
