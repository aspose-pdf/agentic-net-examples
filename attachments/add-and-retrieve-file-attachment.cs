using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify that the source PDF and the file to attach exist
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileSpecification representing the attachment
            // First argument: path to the file on disk
            // Second argument: name that will appear inside the PDF
            FileSpecification fileSpec = new FileSpecification(attachmentFilePath, Path.GetFileName(attachmentFilePath));

            // Add the attachment to the PDF's EmbeddedFiles collection
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF with the new attachment
            pdfDoc.Save(outputPdfPath);

            // Retrieve the first attachment using index 0 as requested
            FileSpecification retrievedAttachment = pdfDoc.EmbeddedFiles[0];

            // Display basic information about the retrieved attachment
            Console.WriteLine($"Retrieved attachment file name: {retrievedAttachment.Name}");
            Console.WriteLine($"Description (if any): {retrievedAttachment.Description}");
        }
    }
}
