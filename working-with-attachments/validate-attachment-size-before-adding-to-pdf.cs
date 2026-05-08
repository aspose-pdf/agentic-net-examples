using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentPath = "attachment.zip";

        // Define maximum allowed attachment size (e.g., 5 MB)
        const long maxAttachmentSizeBytes = 5L * 1024 * 1024;

        // Verify that the source files exist
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

        // Check attachment size before adding it to the PDF
        long attachmentSize = new FileInfo(attachmentPath).Length;
        if (attachmentSize > maxAttachmentSizeBytes)
        {
            Console.Error.WriteLine($"Attachment exceeds size limit ({maxAttachmentSizeBytes} bytes). Size: {attachmentSize} bytes.");
            return;
        }

        // Load the PDF, add the attachment, and save
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use the first page for the annotation (adjust as needed)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle area for the attachment annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a FileSpecification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentPath);

            // Create the FileAttachment annotation and add it to the page
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec);
            page.Annotations.Add(attachment);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}