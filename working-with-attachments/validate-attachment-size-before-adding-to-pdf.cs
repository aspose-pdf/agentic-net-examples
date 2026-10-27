using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Define maximum allowed attachment size (e.g., 5 MB)
        const long maxAttachmentSizeBytes = 5L * 1024 * 1024;

        // List of files to attach
        string[] filesToAttach = { "doc1.pdf", "image.png", "largefile.zip" };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            foreach (string filePath in filesToAttach)
            {
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"Attachment not found: {filePath}");
                    continue;
                }

                // Validate file size before adding
                long fileSize = new FileInfo(filePath).Length;
                if (fileSize > maxAttachmentSizeBytes)
                {
                    Console.Error.WriteLine($"Skipping '{filePath}' (size {fileSize} bytes exceeds limit of {maxAttachmentSizeBytes} bytes).");
                    continue;
                }

                // Add the attachment to the PDF using the EmbeddedFiles collection
                var fileSpec = new FileSpecification(Path.GetFileName(filePath))
                {
                    // Store the file data in a memory stream
                    Contents = new MemoryStream(File.ReadAllBytes(filePath)),
                    // Optional: a description shown in PDF viewers
                    Description = $"Embedded file: {Path.GetFileName(filePath)}"
                };

                pdfDoc.EmbeddedFiles.Add(fileSpec);
                Console.WriteLine($"Attached '{filePath}' ({fileSize} bytes).");
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF saved with attachments to '{outputPdfPath}'.");
        }
    }
}
