using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string attachmentPath = "attachment.txt";     // file to attach
        const string attachmentLabel = "Project Documentation"; // descriptive label
        const string outputPdfPath  = "output.pdf";         // result PDF

        // Verify required files exist
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

        // Load the source PDF
        Document pdfDoc = new Document(inputPdfPath);

        // Create a FileSpecification for the attachment, set its description, and provide the file data via a stream
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath), attachmentLabel);
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));

        // Add the file specification to the EmbeddedFiles collection (Attachments collection does not exist)
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the modified PDF
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}