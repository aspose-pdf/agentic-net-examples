using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_attached.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string attachmentDescription = "Sample attachment";

        // Verify that all required files exist
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

        try
        {
            // Load the existing PDF document
            Document pdfDoc = new Document(inputPdfPath);

            // Create a file specification for the attachment
            var fileSpec = new FileSpecification(attachmentFilePath, attachmentDescription)
            {
                // Optional: set MIME type based on the file type
                MIMEType = "text/plain"
            };

            // Add the file specification to the EmbeddedFiles collection
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF to the desired output path
            pdfDoc.Save(outputPdfPath);

            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
