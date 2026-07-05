using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // source PDF
        const string attachmentPath    = "attachment.bin";     // file to attach
        const string outputPdfPath     = "output_with_attachment.pdf";
        const string customMimeType    = "application/custom-type"; // desired MIME type

        // Verify files exist
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

        // Load the PDF document, add the attachment with a custom MIME type, and save.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment.
            // The constructor that takes a file path sets the Name and FileSystem properties.
            FileSpecification fileSpec = new FileSpecification(attachmentPath);

            // Set the custom MIME type.
            fileSpec.MIMEType = customMimeType;

            // Optionally, you can set a description for the attachment.
            fileSpec.Description = "Custom attachment with MIME type";

            // Add the file specification to the document's embedded files collection.
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment and custom MIME type: {outputPdfPath}");
    }
}