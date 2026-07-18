using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for Permissions enum

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // existing PDF
        const string attachmentPath = "secret.txt";         // file to attach
        const string outputPdfPath  = "output_encrypted.pdf";

        const string userPassword  = "user123";            // password required to open
        const string ownerPassword = "owner123";           // owner password

        // Verify source files exist
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

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // ------------------------------------------------------------
                // Add the attachment to the PDF.
                // The attachment is added to the document's EmbeddedFiles collection.
                // The content will be encrypted together with the document when we
                // apply password protection below.
                // ------------------------------------------------------------
                // Create a FileSpecification using the file path and a description.
                var fileSpec = new FileSpecification(attachmentPath, "Attachment");
                // Assign the file's bytes to the Contents stream.
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
                // Add the file specification to the PDF.
                doc.EmbeddedFiles.Add(fileSpec);

                // ------------------------------------------------------------
                // Encrypt the entire PDF (including attachments) using AES-256.
                // Permissions can be adjusted as needed; here we allow printing
                // and content extraction for the user password.
                // ------------------------------------------------------------
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF. The attachment's stream is now protected.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Encrypted PDF with attachment saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
