using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string attachmentDescription = "Sample text attachment";
        const string customMimeType = "text/plain";

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

        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a file specification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentFilePath, attachmentDescription);
            // Set a custom MIME type for the attachment
            fileSpec.MIMEType = customMimeType;

            // Add the file specification to the PDF's embedded files collection
            pdfDocument.EmbeddedFiles.Add(fileSpec);

            // Save the updated PDF
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added with MIME type '{customMimeType}' and saved to '{outputPdfPath}'.");
    }
}