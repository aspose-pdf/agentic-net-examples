using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string outputPdfPath      = "output.pdf";

        // Verify input files exist
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
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Open the attachment file stream
                using (FileStream attStream = File.OpenRead(attachmentFilePath))
                {
                    // Create a FileSpecification for the attachment
                    var fileSpec = new FileSpecification(
                        attStream,
                        Path.GetFileName(attachmentFilePath), // file name as it will appear in the PDF
                        "Embedded attachment")                // description
                    {
                        // Set MIME type (optional, generic binary here)
                        MIMEType = "application/octet-stream",
                        // Set the relationship type to Data as required
                        AFRelationship = AFRelationship.Data
                    };

                    // Add the attachment to the PDF's EmbeddedFiles collection
                    pdfDoc.EmbeddedFiles.Add(fileSpec);
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachment added successfully. Saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}