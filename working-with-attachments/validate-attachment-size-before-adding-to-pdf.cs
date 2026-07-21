using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Maximum allowed attachment size in bytes (e.g., 5 MB)
    const long MaxAttachmentSizeBytes = 5L * 1024 * 1024;

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Files to attach
        string[] attachmentFiles = { "doc1.pdf", "image.png", "largefile.zip" };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF inside a using block (ensures disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use the first page as the target for the attachment annotation
            Page targetPage = pdfDoc.Pages[1]; // 1‑based indexing

            foreach (string filePath in attachmentFiles)
            {
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"Attachment file not found: {filePath}");
                    continue;
                }

                // Validate file size before creating the attachment
                long fileSize = new FileInfo(filePath).Length;
                if (fileSize > MaxAttachmentSizeBytes)
                {
                    Console.Error.WriteLine($"Skipping attachment (size exceeds limit): {filePath} ({fileSize} bytes)");
                    continue;
                }

                // Create a FileSpecification for the attachment
                FileSpecification fileSpec = new FileSpecification(filePath);

                // Define a rectangle for the annotation (position on the page)
                // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

                // Create the FileAttachment annotation and add it to the page
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(targetPage, rect, fileSpec);
                targetPage.Annotations.Add(attachment);

                Console.WriteLine($"Attached file: {Path.GetFileName(filePath)} ({fileSize} bytes)");
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachments: {outputPdfPath}");
    }
}