using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (required for FileSpecification and Document)

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the file to attach, and the resulting PDF
        const string inputPdfPath      = "input.pdf";
        const string attachmentFilePath = "attachment.bin";
        const string outputPdfPath     = "output.pdf";

        // Attachment metadata
        const string attachmentName = "myAttachment.bin";
        const string description    = "Custom attachment with a specific MIME type";
        const string customMimeType = "application/custom-type";

        // Verify that the source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Open the PDF, add the attachment with the custom MIME type, and save the result
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentFilePath)
            {
                Name        = attachmentName,   // The name that will appear in the attachment list
                Description = description,      // Optional description shown in the UI
                MIMEType    = customMimeType    // <-- custom MIME type
            };

            // Add the specification to the document's embedded files collection
            pdfDocument.EmbeddedFiles.Add(fileSpec);

            // Persist the changes
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added with MIME type '{customMimeType}'. Saved to '{outputPdfPath}'.");
    }
}