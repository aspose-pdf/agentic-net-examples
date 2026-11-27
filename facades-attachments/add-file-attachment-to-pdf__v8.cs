using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string attachmentFile = "attachment.txt";
        const string outputPdf = "attached.pdf";

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

        // Load the PDF document.
        Document pdf = new Document(sourcePdf);

        // Create a file specification for the attachment.
        var fileSpec = new FileSpecification(attachmentFile)
        {
            Description = Path.GetFileName(attachmentFile),
            // Load the file contents into a memory stream.
            Contents = new MemoryStream(File.ReadAllBytes(attachmentFile))
        };

        // Add the attachment to the PDF.
        pdf.EmbeddedFiles.Add(fileSpec);

        // Save the PDF with the attached file.
        pdf.Save(outputPdf);

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}
