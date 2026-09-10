using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // only needed if you still want to use PdfContentEditor for other edits

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // source PDF
        const string attachmentFile = "sample.dat";        // file to attach
        const string outputPdf = "output_with_attachment.pdf";
        const string customMime = "application/custom-mime"; // desired MIME type (NOTE: Aspose.Pdf does not expose a MimeType property; this value is kept for documentation purposes only)

        // Ensure the source PDF and attachment exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Load the existing PDF document
        Document doc = new Document(inputPdf);
        Page page = doc.Pages[1]; // 1‑based indexing

        // ---------------------------------------------------------------------
        // Create a FileSpecification for the attachment. The constructor sets the
        // file path and a description. Aspose.Pdf does not expose a public MimeType
        // property, so the MIME type cannot be set directly via the API. If you need
        // the MIME type to be present in the PDF, you would have to manipulate the
        // underlying PDF objects manually (out of scope for this example).
        // ---------------------------------------------------------------------
        FileSpecification fileSpec = new FileSpecification(attachmentFile, "Sample attachment");
        // Optional: you can set a custom display name if you do not want the file name
        // to be used as the attachment name.
        // fileSpec.Name = "my_custom_name.dat"; // uncomment if needed

        // Create the file attachment annotation. The rectangle defines the icon
        // location on the page (lower‑left X/Y, upper‑right X/Y).
        var rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);
        FileAttachmentAnnotation fileAnn = new FileAttachmentAnnotation(page, rect, fileSpec)
        {
            // Choose an icon – Graph, PushPin, Paperclip, Tag are valid values.
            Icon = FileIcon.Graph,
            // The annotation's contents (tooltip) can be set via the "Contents" property.
            Contents = "Sample attachment"
        };

        // Add the annotation to the page.
        page.Annotations.Add(fileAnn);

        // Save the modified PDF.
        doc.Save(outputPdf);

        Console.WriteLine($"PDF saved with attachment (custom MIME type noted in comments): {outputPdf}");
    }
}
