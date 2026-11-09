using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "document.pdf";          // PDF to which the attachment will be added
        const string attachmentPath = "data.bin";      // File to attach
        const string description = "Custom binary data";
        const string customMimeType = "application/x-custom-binary"; // Your custom MIME type

        // Verify that both source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Load the PDF document using the Document class (PdfFileEditor does not support AttachFile)
        Document pdfDoc = new Document(pdfPath);

        // Create a FileSpecification for the attachment and set the custom MIME type
        var fileSpec = new FileSpecification(attachmentPath, description)
        {
            MIMEType = customMimeType
        };

        // Add the file specification to the EmbeddedFiles collection
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the updated PDF (overwrites the original file)
        pdfDoc.Save(pdfPath);

        Console.WriteLine($"Attachment added to '{pdfPath}' with MIME type '{customMimeType}'.");
    }
}