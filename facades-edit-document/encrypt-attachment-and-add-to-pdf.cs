using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths
        const string sourcePdfPath = "source.pdf";               // PDF to receive the attachment
        const string attachmentPath = "attachment.pdf";           // Original attachment file
        const string encryptedAttachmentPath = "attachment_encrypted.pdf"; // Encrypted version of the attachment
        const string outputPdfPath = "result.pdf";               // Final PDF with encrypted attachment

        // Passwords for the attachment encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // -----------------------------------------------------------------
        // Step 0: Verify that the source PDF and the attachment exist
        // -----------------------------------------------------------------
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Encrypt the attachment file using Document.Encrypt (modern API)
        // -----------------------------------------------------------------
        try
        {
            // Load the attachment PDF
            Document attachmentDoc = new Document(attachmentPath);

            // Encrypt with 256‑bit AES, allow printing only
            attachmentDoc.Encrypt(userPassword, ownerPassword, Permissions.PrintDocument, CryptoAlgorithm.AESx256);

            // Save the encrypted version
            attachmentDoc.Save(encryptedAttachmentPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to encrypt the attachment: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 2: Add the encrypted attachment to the target PDF
        // -----------------------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            // Bind the source PDF that will receive the attachment
            editor.BindPdf(sourcePdfPath);

            // Add the encrypted file as an attachment (no visual annotation)
            editor.AddDocumentAttachment(encryptedAttachmentPath, "Encrypted attachment");

            // Save the resulting PDF
            editor.Save(outputPdfPath);
        }
        finally
        {
            // Close the editor to release resources
            editor.Close();
        }

        Console.WriteLine($"Attachment encrypted and added successfully. Output: {outputPdfPath}");
    }
}
