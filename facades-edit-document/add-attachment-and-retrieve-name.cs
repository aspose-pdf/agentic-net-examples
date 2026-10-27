using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the output PDF (with attachment) and the file to attach
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string attachmentFilePath = "sample.txt";
        const string attachmentName = "SampleAttachment.txt";

        // Verify that the required files exist
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

        try
        {
            // Create a copy of the source PDF to avoid modifying the original file
            File.Copy(inputPdfPath, outputPdfPath, overwrite: true);

            // Load the copied PDF using Document (PdfFileEditor does not support attachments)
            Document pdfDoc = new Document(outputPdfPath);

            // Add the attachment using the EmbeddedFiles collection.
            // The FileSpecification.Name property defines the name that will appear in the PDF.
            var fileSpec = new FileSpecification(attachmentName)
            {
                // Load the file content into a stream; this is required for embedding.
                Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath))
            };
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the changes back to the output file
            pdfDoc.Save(outputPdfPath);

            // Retrieve the list of embedded file names from the PDF for verification
            Document verifyDoc = new Document(outputPdfPath);
            string[] attachedNames = verifyDoc.EmbeddedFiles
                                            .Select(fs => fs.Name)
                                            .ToArray();

            // Verify that the expected attachment name is present
            bool attachmentFound = attachedNames != null && Array.IndexOf(attachedNames, attachmentName) >= 0;

            Console.WriteLine(attachmentFound
                ? $"Attachment '{attachmentName}' added and verified successfully."
                : $"Attachment '{attachmentName}' was not found in the PDF.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
