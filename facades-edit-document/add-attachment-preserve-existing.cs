using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";          // existing PDF
        const string attachmentFile = "newAttachment.txt";  // file to attach
        const string outputPdf = "output.pdf";         // PDF with new attachment

        // Verify that source files exist
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

        // Load the existing PDF using Document (PdfFileEditor does not support AddAttachment)
        Document pdfDoc = new Document(sourcePdf);

        // Create a FileSpecification for the new attachment
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentFile))
        {
            Description = Path.GetFileName(attachmentFile),
            // Load the file data into the specification
            Contents = new MemoryStream(File.ReadAllBytes(attachmentFile))
        };

        // Add the attachment – existing embedded files are preserved automatically
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the result to a new file (original PDF remains unchanged)
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdf}'.");
    }
}
