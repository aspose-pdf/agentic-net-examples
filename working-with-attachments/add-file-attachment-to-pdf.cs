using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPdfPath  = "output_with_attachment.pdf";

        // Verify files exist
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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the attachment annotation will be placed (first page)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle that represents the annotation's border
            // (left, bottom, right, top) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create a FileSpecification for the file to be attached
            FileSpecification fileSpec = new FileSpecification(attachmentPath, "Sample attachment");

            // Create the FileAttachmentAnnotation with the page, rectangle, and file specification
            FileAttachmentAnnotation attachmentAnnotation = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional: set the icon that will be shown in the PDF viewer
                Icon = FileIcon.Paperclip,
                // Optional: provide a tooltip text
                Contents = "Attached file: attachment.txt"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(attachmentAnnotation);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdfPath}");
    }
}
