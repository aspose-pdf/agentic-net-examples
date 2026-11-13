using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string attachmentPath = "attachment.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the attachment annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation (position and size)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 150, 750);

            // Create the file specification using the constructor that accepts the file path and a description
            FileSpecification fileSpec = new FileSpecification(attachmentPath, "Sample PDF attachment");
            // The Description property can also be set explicitly if needed
            fileSpec.Description = "Sample PDF attachment";

            // Create the file attachment annotation, passing the page, rectangle and file specification
            FileAttachmentAnnotation fileAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional: set an icon for the attachment
                Icon = FileIcon.PushPin
            };

            // Add the annotation to the page
            page.Annotations.Add(fileAnnot);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdfPath}");
    }
}
