using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string attachmentPath = "attachment.bin";    // file to attach
        const string outputPdfPath = "output_with_attachment.pdf";

        // Ensure source files exist
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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the attachment annotation will appear
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle for the annotation (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a file specification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentPath);
            // Set a Unicode filename (e.g., Chinese characters)
            fileSpec.UnicodeName = "文件附件.bin";

            // Create the file attachment annotation
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional visual settings
                Icon = FileIcon.Graph, // use enum type name, not instance reference
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Embedded file with Unicode name"
            };

            // Add the annotation to the page
            page.Annotations.Add(attachment);

            // Save the modified PDF (lifecycle rule: use Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdfPath}");
        // Simple verification: check that the annotation exists
        using (Document verifyDoc = new Document(outputPdfPath))
        {
            int count = verifyDoc.Pages[1].Annotations.Count;
            Console.WriteLine($"Page 1 contains {count} annotation(s).");
        }
    }
}
