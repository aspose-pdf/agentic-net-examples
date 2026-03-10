using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string fileToAttachPath = "attachment.txt";
        const string attachmentName = "attachment.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ---------- Advanced operation: add an embedded file ----------
            if (File.Exists(fileToAttachPath))
            {
                // Create FileSpecification from file path (not from stream)
                FileSpecification fileSpec = new FileSpecification(fileToAttachPath, attachmentName);
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }
            else
            {
                Console.Error.WriteLine($"Attachment file not found: {fileToAttachPath}");
            }

            // ---------- Remove an attachment by name ----------
            try
            {
                // Delete the attachment (if it exists)
                pdfDoc.EmbeddedFiles.Delete(attachmentName);
            }
            catch (Exception ex)
            {
                // If the attachment does not exist, ignore the error
                Console.Error.WriteLine($"Could not delete attachment '{attachmentName}': {ex.Message}");
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
    }
}
