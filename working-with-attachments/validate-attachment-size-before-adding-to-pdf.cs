using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Maximum allowed attachment size (in bytes). Example: 5 MB.
    const long MaxAttachmentSizeBytes = 5L * 1024 * 1024;

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_attachments.pdf";

        // Paths of files to be attached.
        string[] attachmentFiles = { "doc1.pdf", "image.png", "large_file.zip" };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use the first page for demonstration; adjust as needed.
            Page page = pdfDoc.Pages[1];

            foreach (string filePath in attachmentFiles)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Attachment not found, skipping: {filePath}");
                    continue;
                }

                // Check file size against the limit.
                long fileSize = new FileInfo(filePath).Length;
                if (fileSize > MaxAttachmentSizeBytes)
                {
                    Console.WriteLine($"Attachment exceeds size limit ({MaxAttachmentSizeBytes} bytes), skipping: {filePath}");
                    continue;
                }

                // Create a FileSpecification describing the attachment.
                FileSpecification fileSpec = new FileSpecification(filePath);

                // Define the rectangle where the annotation will appear.
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

                // Create the FileAttachment annotation.
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Optional visual styling.
                    Color = Aspose.Pdf.Color.LightGray,
                    Title = Path.GetFileName(filePath),
                    Contents = $"Attached file: {Path.GetFileName(filePath)}"
                };

                // Add the annotation to the page.
                page.Annotations.Add(attachment);

                Console.WriteLine($"Attached file: {filePath} ({fileSize} bytes)");
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachments: {outputPdfPath}");
    }
}