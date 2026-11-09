using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string attachmentPath = "attachment.txt";
        const string attachmentName = "MyAttachment.txt";

        // Verify that the source PDF and the attachment exist
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

        // Load the PDF using Document (PdfFileEditor does not provide AddAttachment)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a file specification for the attachment using the correct constructor
            FileSpecification fileSpec = new FileSpecification(attachmentPath, attachmentName)
            {
                Description = attachmentName
            };

            // Optional: set the modification date of the attachment
            fileSpec.Params.ModDate = DateTime.UtcNow;

            // Add the attachment to the EmbeddedFiles collection (defines it as an embedded file)
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}
