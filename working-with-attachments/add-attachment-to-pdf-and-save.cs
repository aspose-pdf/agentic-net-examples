using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "input.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string outputFolder = "Output";

        // Validate input files
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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Destination PDF file
        string outputPdfPath = Path.Combine(outputFolder, "output_with_attachments.pdf");

        // Load, modify, and save the PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment
            var fileSpec = new FileSpecification(attachmentFilePath, Path.GetFileName(attachmentFilePath));
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));

            // Add the attachment to the document's EmbeddedFiles collection
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the modified document to the specified output folder
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachments to: {outputPdfPath}");
    }
}
