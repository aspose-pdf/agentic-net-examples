using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // existing PDF
        const string attachmentPath = "attachment.txt"; // file to attach
        const string outputPdfPath = "output_with_attachment.pdf";

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

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentPath);

            // Define the rectangle that will host the attachment annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create the FileAttachmentAnnotation on the first page (1‑based indexing)
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec)
            {
                // Optional: set an icon – use the supported FileIcon enum
                Icon = FileIcon.Paperclip,
                // Optional: provide a tooltip / description
                Contents = "Attached file: attachment.txt"
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(attachment);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdfPath}");
    }
}
