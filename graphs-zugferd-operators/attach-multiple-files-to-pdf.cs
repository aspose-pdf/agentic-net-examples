using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AttachMultipleFiles
{
    static void Main()
    {
        // Input PDF and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Files to attach with their MIME types and descriptions
        var attachments = new[]
        {
            new { Path = "file1.txt",  Mime = "text/plain",          Description = "Text file attachment" },
            new { Path = "image1.jpg", Mime = "image/jpeg",          Description = "JPEG image attachment" },
            new { Path = "doc1.pdf",   Mime = "application/pdf",    Description = "PDF document attachment" }
        };

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: load)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Choose the page where the annotations will be placed (first page in this example)
            Page page = pdfDoc.Pages[1];

            // Position for each attachment annotation (stacked vertically)
            double yTop = 800; // start from top of the page
            const double xLeft = 50;
            const double width  = 30;
            const double height = 30;
            const double verticalSpacing = 40;

            foreach (var att in attachments)
            {
                if (!File.Exists(att.Path))
                {
                    Console.Error.WriteLine($"Attachment file not found: {att.Path}");
                    continue; // skip missing files
                }

                // Create a FileSpecification for the attachment using the constructor that accepts path and description
                FileSpecification fileSpec = new FileSpecification(att.Path, att.Description);
                // NOTE: The MimeType property is not available in the current Aspose.Pdf version, so it is omitted.

                // Define the rectangle for the annotation (border area)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    xLeft,
                    yTop - height,
                    xLeft + width,
                    yTop
                );

                // Create the FileAttachment annotation
                FileAttachmentAnnotation fileAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Subject is displayed as the attachment description in PDF viewers
                    Subject = att.Description,
                    // Title appears in the annotation popup window
                    Title   = Path.GetFileName(att.Path)
                };

                // Use the FileIcon enum instead of a raw string
                fileAnnot.Icon = FileIcon.PushPin;

                // Add the annotation to the page
                page.Annotations.Add(fileAnnot);

                // Move down for the next annotation
                yTop -= verticalSpacing;
            }

            // Save the modified PDF (lifecycle rule: save)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Attachments added and saved to '{outputPdf}'.");
    }
}
