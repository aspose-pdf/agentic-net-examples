using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths
        const string sourcePdf = "source.pdf";               // PDF to which the attachment will be added
        const string attachmentPdf = "attachment.pdf";       // Original attachment (must be a PDF)
        const string encryptedAttachment = "attachment_encrypted.pdf"; // Temp encrypted file
        const string outputPdf = "result.pdf";               // Final PDF with encrypted attachment

        // Passwords for the attachment encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // -----------------------------------------------------------------
        // Step 0: Verify the attachment exists
        // -----------------------------------------------------------------
        if (!File.Exists(attachmentPdf))
        {
            Console.Error.WriteLine($"Attachment file '{attachmentPdf}' not found.");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Encrypt the attachment PDF using Document.Encrypt (recommended API)
        // -----------------------------------------------------------------
        try
        {
            // Load the attachment PDF
            using (Document attachmentDoc = new Document(attachmentPdf))
            {
                // Define the permissions you want to allow (example: printing only)
                var permissions = Permissions.PrintDocument;

                // Encrypt with 256‑bit AES
                attachmentDoc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted version to a temporary file
                attachmentDoc.Save(encryptedAttachment);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to encrypt the attachment: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 2: Add the encrypted attachment to the main PDF
        // -----------------------------------------------------------------
        try
        {
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the source PDF
                editor.BindPdf(sourcePdf);

                // Attach the encrypted file (no visual annotation)
                editor.AddDocumentAttachment(encryptedAttachment, "Encrypted attachment");

                // Save the resulting PDF
                editor.Save(outputPdf);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to add attachment to PDF: {ex.Message}");
            return;
        }

        // Optional: clean up the temporary encrypted file
        try
        {
            File.Delete(encryptedAttachment);
        }
        catch
        {
            // Ignore any errors during cleanup
        }

        Console.WriteLine($"Attachment encrypted and added. Output saved to '{outputPdf}'.");
    }
}
