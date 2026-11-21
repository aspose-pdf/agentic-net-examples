using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string attachmentFilePath = "attachment.txt"; // file to attach
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify files exist
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

        // Load the PDF
        Document pdfDoc = new Document(inputPdfPath);

        // Create a FileSpecification for the embedded file using the correct constructor
        var fileSpec = new FileSpecification(attachmentFilePath, "Added by Aspose.Pdf")
        {
            Description = "Added by Aspose.Pdf"
        };
        // Set the modification (audit) date via Params.ModDate
        fileSpec.Params.ModDate = DateTime.UtcNow;
        // Load the file contents into a stream
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));

        // Add the file specification to the EmbeddedFiles collection
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the updated PDF
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}
