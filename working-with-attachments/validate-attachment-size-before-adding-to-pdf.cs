using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Maximum allowed attachment size in bytes (e.g., 10 MB)
    const long MaxAttachmentSize = 10L * 1024 * 1024;

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentPath = "attachment.zip";

        // Verify source files exist
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

        // Check attachment size against the limit
        long attachmentSize = new FileInfo(attachmentPath).Length;
        if (attachmentSize > MaxAttachmentSize)
        {
            Console.Error.WriteLine($"Attachment exceeds size limit ({MaxAttachmentSize} bytes). Size: {attachmentSize} bytes.");
            return;
        }

        // Load the PDF, add the attachment, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a FileSpecification for the attachment
            using (FileStream fs = new FileStream(attachmentPath, FileMode.Open, FileAccess.Read))
            {
                FileSpecification fileSpec = new FileSpecification(fs, Path.GetFileName(attachmentPath));

                // Create the FileAttachment annotation and add it to the page
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec);
                page.Annotations.Add(attachment);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}