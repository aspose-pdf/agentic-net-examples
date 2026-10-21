using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";          // original PDF
        const string attachmentFile = "data.bin";      // file to attach
        const string outputPdf = "output.pdf";        // PDF with attachment
        const string description = "Custom data attachment";
        const string customMimeType = "application/x-custom-data"; // custom MIME type

        // Verify input files exist
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Load the source PDF using Document (PdfFileEditor does not support AddAttachment)
        Document pdfDoc = new Document(sourcePdf);

        // Create a file specification for the attachment, set its contents and the custom MIME type
        FileSpecification fileSpec = new FileSpecification(Path.GetFileName(attachmentFile), description)
        {
            Contents = new MemoryStream(File.ReadAllBytes(attachmentFile)),
            MIMEType = customMimeType
        };

        // Add the attachment to the PDF via the EmbeddedFiles collection
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the modified PDF to the output path
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"Attachment added with MIME type '{customMimeType}' to '{outputPdf}'.");
    }
}
