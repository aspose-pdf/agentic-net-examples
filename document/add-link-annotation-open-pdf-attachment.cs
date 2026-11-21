using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath   = "source.pdf";          // PDF to which we add the annotation
        const string attachmentPath  = "attachment.pdf";      // PDF file to embed
        const string outputPdfPath   = "output_with_link.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment PDF not found: {attachmentPath}");
            return;
        }

        // Load the main document
        using (Document doc = new Document(sourcePdfPath))
        {
            // ------------------------------------------------------------
            // Embed the attachment PDF into the document
            // ------------------------------------------------------------
            // Create a FileSpecification for the attachment and add it to the
            // document's EmbeddedFiles collection.
            FileSpecification fileSpec = new FileSpecification(attachmentPath, "Embedded PDF attachment");
            fileSpec.Params.ModDate = DateTime.UtcNow;
            doc.EmbeddedFiles.Add(fileSpec);

            // ------------------------------------------------------------
            // Create an annotation that opens the embedded PDF when clicked
            // ------------------------------------------------------------
            // A FileAttachmentAnnotation displays an attachment icon; clicking it
            // opens the embedded file. This satisfies the requirement of a clickable
            // element that opens the attached PDF.
            Page page = doc.Pages[1]; // first page (1‑based indexing)

            // Define the clickable rectangle (llx, lly, urx, ury) in points.
            // Adjust coordinates as needed for your layout.
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create the file‑attachment annotation.
            FileAttachmentAnnotation attachmentAnn = new FileAttachmentAnnotation(page, linkRect, fileSpec)
            {
                Title    = "Open attached PDF",
                Contents = "Click to open the embedded PDF document.",
                // Use the FileIcon enum instead of a raw string.
                Icon = FileIcon.Paperclip
            };

            // Add the annotation to the page.
            page.Annotations.Add(attachmentAnn);

            // ------------------------------------------------------------
            // Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment annotation: {outputPdfPath}");
    }
}
