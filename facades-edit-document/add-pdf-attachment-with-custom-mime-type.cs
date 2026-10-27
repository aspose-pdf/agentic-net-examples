using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source PDF
        const string attachmentPath = "invoice.pdf";           // file to attach
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDoc = new Document(inputPdfPath);

        // Create a FileSpecification for the attachment with custom MIME type and description.
        FileSpecification fileSpec = new FileSpecification(attachmentPath, "Invoice Document")
        {
            MIMEType = "application/pdf",
            // Optional: set relationship if you want to mark it as data.
            // AFRelationship = AFRelationship.Data
        };

        // Add the file specification to the EmbeddedFiles collection.
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the modified PDF.
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"Attachment added. Output saved to '{outputPdfPath}'.");
    }
}
