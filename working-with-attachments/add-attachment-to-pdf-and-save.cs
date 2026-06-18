using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "input.pdf";               // Existing PDF
        const string attachmentFilePath = "attachment.txt";    // File to attach
        const string outputFolder = "output";                  // Destination folder

        // Validate inputs
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

        // Build the full output file path
        string outputPdfPath = Path.Combine(outputFolder, "output_with_attachments.pdf");

        // Load the PDF, add the attachment (as an embedded file), and save
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a FileSpecification using the constructor that accepts the file path and description
            var fileSpec = new FileSpecification(attachmentFilePath, Path.GetFileName(attachmentFilePath));
            // Optional: set MIME type if needed, e.g. fileSpec.MimeType = "text/plain";

            // Add the file specification to the document's EmbeddedFiles collection
            pdfDocument.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF to the specified folder
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachments to: {outputPdfPath}");
    }
}
