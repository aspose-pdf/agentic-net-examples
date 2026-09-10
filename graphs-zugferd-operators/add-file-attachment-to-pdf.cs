using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // source PDF
        const string attachmentPdfPath = "attachment.pdf";     // file to attach
        const string outputPdfPath     = "output_with_attachment.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentPdfPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that represents the annotation's border
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a FileSpecification for the attached file using the (filePath, description) constructor
            FileSpecification fileSpec = new FileSpecification(attachmentPdfPath, "Attached PDF document");

            // Create the FileAttachment annotation
            FileAttachmentAnnotation fileAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Description of the attachment (appears in the annotation's popup)
                Subject = "Attached PDF document",
                // Optional title shown in the annotation's title bar
                Title   = "Attachment"
            };

            // Add the annotation to the page
            page.Annotations.Add(fileAnnot);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdfPath}");
    }
}
