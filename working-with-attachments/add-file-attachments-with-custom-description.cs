using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Define attachments with custom description and MIME type (MIME type is inferred from file extension)
        var attachments = new[]
        {
            new { FilePath = "file1.pdf", Description = "First attachment (PDF)" },
            new { FilePath = "image1.png", Description = "Sample image attachment" }
        };

        // Load existing PDF or create a new one if it does not exist
        Document pdfDocument;
        if (File.Exists(inputPdfPath))
        {
            pdfDocument = new Document(inputPdfPath);
        }
        else
        {
            pdfDocument = new Document();
            pdfDocument.Pages.Add(); // ensure at least one page exists
        }

        // Use the first page for placing attachment annotations
        Page page = pdfDocument.Pages[1];

        // Starting position for the first attachment annotation
        double startX = 50;
        double startY = 700;
        double annotWidth = 20;
        double annotHeight = 20;
        double spacing = 10;

        foreach (var att in attachments)
        {
            // Create a FileSpecification using the constructor that accepts file path and description
            // MIME type is automatically determined from the file extension by Aspose.Pdf
            FileSpecification fileSpec = new FileSpecification(att.FilePath, att.Description);

            // Define the rectangle where the annotation will appear
            Rectangle rect = new Rectangle(
                startX,
                startY,
                startX + annotWidth,
                startY + annotHeight);

            // Create the file attachment annotation using the file specification
            FileAttachmentAnnotation fileAttachment = new FileAttachmentAnnotation(page, rect, fileSpec);

            // Optional visual settings – Icon property can be set using a string value if needed.
            // The enum FileAttachmentAnnotationIcon may not be available in all versions, so we omit it.
            // fileAttachment.Icon = "Graph"; // alternative using string literal

            // Tooltip text shown when the user hovers over the annotation
            fileAttachment.Contents = att.Description;

            // Add the annotation to the page
            page.Annotations.Add(fileAttachment);

            // Update position for the next annotation
            startX += annotWidth + spacing;
        }

        // Save the modified PDF
        pdfDocument.Save(outputPdfPath);
    }
}
