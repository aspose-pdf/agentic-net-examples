using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source PDF
        const string attachmentFilePath = "Invoice2023.pdf";   // file to attach
        const string outputPdfPath = "output.pdf";             // result PDF

        // Verify that required files exist
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

        // Load the source PDF using the Document class (the recommended way for manipulation).
        Document pdfDoc = new Document(inputPdfPath);

        // Create a FileSpecification for the attachment.
        // The current Aspose.Pdf API does not expose CreationDate/ModDate on FileSpecification,
        // so we embed the creation timestamp in the description (or a custom dictionary if needed).
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentFilePath))
        {
            Description = $"Invoice2023 (Created: {DateTime.Now:O})",
            // Load the file contents into a memory stream as required by the EmbeddedFiles collection.
            Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath))
        };

        // Add the attachment to the PDF via the EmbeddedFiles collection.
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the modified PDF, overwriting any existing output file.
        if (File.Exists(outputPdfPath))
            File.Delete(outputPdfPath);
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}
