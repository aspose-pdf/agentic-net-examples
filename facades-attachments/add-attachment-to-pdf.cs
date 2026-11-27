using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentPath = "attachment.txt";

        // Verify required files exist
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

        // Load the PDF using the high‑level Document API
        Document pdf = new Document(inputPdfPath);

        // Create a file specification for the attachment
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
        {
            Description = "Embedded attachment",
            // Load the attachment bytes into a memory stream
            Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
        };

        // Add the attachment to the PDF's EmbeddedFiles collection
        pdf.EmbeddedFiles.Add(fileSpec);

        // Save the updated PDF
        pdf.Save(outputPdfPath);

        Console.WriteLine($"PDF saved with attachment to '{outputPdfPath}'.");
    }
}
