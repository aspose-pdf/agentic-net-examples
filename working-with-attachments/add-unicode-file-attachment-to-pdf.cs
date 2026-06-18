using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for source PDF, attachment file, and output PDF
        const string inputPdfPath = "input.pdf";
        const string attachmentFilePath = "sample.txt";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Unicode filename to be displayed in PDF viewers
        const string unicodeFileName = "文件附件.txt";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment and set its Unicode name
            FileSpecification fileSpec = new FileSpecification(attachmentFilePath);
            fileSpec.UnicodeName = unicodeFileName; // Unicode filename

            // Define the rectangle where the attachment icon will appear (first page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create the FileAttachment annotation on page 1
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec)
            {
                // Optional: set a tooltip (Icon property omitted because the enum is not available in the referenced version)
                Contents = "Attached file with Unicode name"
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(attachment);

            // Save the modified PDF (lifecycle rule: use Save within using)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with Unicode attachment saved to '{outputPdfPath}'.");
    }
}
