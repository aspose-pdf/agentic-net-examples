using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPath = "attachment.zip";
        const long maxAttachmentSize = 5 * 1024 * 1024; // 5 MB limit

        // Verify source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Check attachment size before adding
        FileInfo attachInfo = new FileInfo(attachmentPath);
        if (attachInfo.Length > maxAttachmentSize)
        {
            Console.Error.WriteLine($"Attachment size ({attachInfo.Length} bytes) exceeds the allowed limit of {maxAttachmentSize} bytes.");
            return;
        }

        // Load the PDF, add the attachment, and save
        using (Document doc = new Document(inputPdf))
        {
            // Use the first page for the annotation (adjust as needed)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a file specification that points to the attachment file
            FileSpecification fileSpec = new FileSpecification(attachmentPath);

            // Create the file attachment annotation
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional: description shown when the annotation is opened
                Contents = $"Attached file: {Path.GetFileName(attachmentPath)}"
                // Icon can be set via the Icon property if a different visual is desired
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(attachment);

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added successfully. Saved to '{outputPdf}'.");
    }
}