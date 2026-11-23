using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentPath = "newAttachment.txt";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Delete all existing embedded file attachments
            doc.EmbeddedFiles.Delete();

            // Add a new file attachment
            // The simplest constructor takes the file path directly.
            // If you need to supply the content manually, assign a MemoryStream to Contents.
            var fileSpec = new FileSpecification(attachmentPath);
            // Example of explicit content assignment (optional):
            // byte[] data = File.ReadAllBytes(attachmentPath);
            // fileSpec.Contents = new MemoryStream(data);

            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
    }
}
