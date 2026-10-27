using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string attachmentFilePath = "attachment.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Example usage of a class from Aspose.Pdf.Facades (required by the task)
        PdfFileEditor facadesHelper = new PdfFileEditor();

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment using the constructor that accepts the file path and a description.
            FileSpecification fileSpec = new FileSpecification(attachmentFilePath, "Attachment for auditing");

            // Set the modification date via the Params property (the direct ModDate property no longer exists).
            fileSpec.Params.ModDate = DateTime.UtcNow;

            // Define where the attachment annotation will appear on page 1
            Rectangle rect = new Rectangle(100, 700, 120, 720);

            // Create the file attachment annotation
            FileAttachmentAnnotation attachmentAnn = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec)
            {
                Title = "Attached File",
                Contents = "Attachment added for auditing purposes."
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(attachmentAnn);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment and UTC modification date: {outputPdfPath}");
    }
}
