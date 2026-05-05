using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";
        const string attachmentPath    = "invoice2023.pdf";
        const string outputPdfPath     = "output.pdf";

        // Verify required files exist
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

        // Use PdfContentEditor facade to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdfPath);

            // Define the annotation rectangle (position and size) – fully qualified to avoid ambiguity
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 100, 100);

            // Create a file attachment annotation on page 1 with description "Invoice2023"
            // Icon name can be any of the supported values ("Graph", "PushPin", "Paperclip", "Tag")
            editor.CreateFileAttachment(rect, "Invoice2023", attachmentPath, 1, "Paperclip");

            // Retrieve the newly added annotation (it will be the last one on the page)
            Page page = editor.Document.Pages[1]; // Pages are 1‑based
            Annotation annotation = page.Annotations[page.Annotations.Count]; // Annotations are also 1‑based

            // Cast to FileAttachmentAnnotation to set the creation date
            if (annotation is FileAttachmentAnnotation fileAttachment)
            {
                fileAttachment.CreationDate = DateTime.Now; // Set to current system time
            }

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added with description 'Invoice2023' and saved to '{outputPdfPath}'.");
    }
}