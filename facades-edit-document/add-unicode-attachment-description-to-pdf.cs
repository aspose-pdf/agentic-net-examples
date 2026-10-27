using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "input.pdf";
        const string attachmentPath = "unicode.txt";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify files exist
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

        // Unicode description for the attachment
        string unicodeDescription = "Описание с Unicode 🚀";

        // Load the source PDF using Document (Facades API does not support AddAttachment)
        Document pdfDoc = new Document(inputPdfPath);

        // Create a FileSpecification for the attachment
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath), Path.GetFileName(attachmentPath));
        fileSpec.Description = unicodeDescription;
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));

        // Add the file specification to the EmbeddedFiles collection
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the modified PDF
        pdfDoc.Save(outputPdfPath);

        // Verify that the description was stored correctly
        Document resultDoc = new Document(outputPdfPath);
        foreach (FileSpecification embedded in resultDoc.EmbeddedFiles)
        {
            Console.WriteLine($"Attachment Name: {embedded.Name}");
            Console.WriteLine($"Attachment Description: {embedded.Description}");
        }
    }
}
