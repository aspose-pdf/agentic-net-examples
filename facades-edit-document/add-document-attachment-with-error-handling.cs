using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the file to attach, and the resulting PDF
        const string sourcePdfPath = "input.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string attachmentName = "MyAttachment.txt";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Verify that the attachment file path is provided and the file exists
        if (string.IsNullOrWhiteSpace(attachmentFilePath) || !File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found or path is invalid: {attachmentFilePath ?? "<null>"}");
            return;
        }

        try
        {
            // Copy the source PDF to the output location so the original file remains unchanged
            File.Copy(sourcePdfPath, outputPdfPath, overwrite: true);

            // Load the copied PDF using the Document API (PdfFileEditor does not support AddDocumentAttachment)
            using (Document pdfDoc = new Document(outputPdfPath))
            {
                // Create a FileSpecification for the attachment using a stream (required by Aspose.Pdf)
                var fileSpec = new FileSpecification(attachmentName);
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));
                fileSpec.Description = "Embedded attachment";

                // Add the file specification to the EmbeddedFiles collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);

                // Save the modified PDF back to the output path
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachment '{attachmentName}' added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., I/O issues, permission problems)
            Console.Error.WriteLine($"Error adding attachment: {ex.Message}");
        }
    }
}
