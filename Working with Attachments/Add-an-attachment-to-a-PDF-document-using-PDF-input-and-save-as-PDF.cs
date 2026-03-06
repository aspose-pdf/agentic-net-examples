using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_with_attachment.pdf"; // result PDF
        const string attachmentPath = "attachment.txt"; // file to embed

        // Verify files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the attachment annotation will be placed (first page)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle that bounds the annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a FileSpecification describing the file to attach
            FileSpecification fileSpec = new FileSpecification(attachmentPath);

            // Create the FileAttachment annotation and add it to the page
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec);
            page.Annotations.Add(attachment);

            // Save the modified document as PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPath}'.");
    }
}
