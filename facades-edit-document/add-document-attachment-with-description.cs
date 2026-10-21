using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.txt";
        const string outputPdf = "output_with_attachment.pdf";
        const string description = "Sample attachment for metadata";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPdf);

        // Create a FileSpecification for the attachment
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentFile))
        {
            Description = description,
            // Embed the file data
            Contents = new MemoryStream(File.ReadAllBytes(attachmentFile))
        };

        // Add the file specification to the EmbeddedFiles collection
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the modified PDF
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdf}'.");
    }
}
