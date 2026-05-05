using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Added for DocumentPrivilege and KeySize enums

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";               // PDF to which the attachment will be added
        const string attachmentPath = "attachment.pdf";          // File to be attached (must be a PDF for encryption)
        const string encryptedAttachmentPath = "attachment_enc.pdf"; // Temporary encrypted version
        const string outputPdfPath = "output.pdf";               // Resulting PDF

        // Verify input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Encrypt the attachment (only PDF files can be encrypted
        //         with PdfFileSecurity). The encrypted file is written to a
        //         temporary location.
        // ------------------------------------------------------------
        using (PdfFileSecurity attachmentSecurity = new PdfFileSecurity())
        {
            attachmentSecurity.BindPdf(attachmentPath);
            // Encrypt with user/owner passwords, allow printing, use 256‑bit AES
            attachmentSecurity.EncryptFile(
                userPassword: "attachUser",
                ownerPassword: "attachOwner",
                privilege: DocumentPrivilege.Print,
                keySize: KeySize.x256);
            attachmentSecurity.Save(encryptedAttachmentPath);
        }

        // ------------------------------------------------------------
        // Step 2: Add the encrypted attachment to the original PDF.
        // ------------------------------------------------------------
        using (PdfContentEditor pdfEditor = new PdfContentEditor())
        {
            pdfEditor.BindPdf(sourcePdfPath);
            pdfEditor.AddDocumentAttachment(encryptedAttachmentPath, "Encrypted attachment");
            pdfEditor.Save(outputPdfPath);
        }

        // Clean up the temporary encrypted file
        if (File.Exists(encryptedAttachmentPath))
        {
            File.Delete(encryptedAttachmentPath);
        }

        Console.WriteLine($"PDF with encrypted attachment saved to '{outputPdfPath}'.");
    }
}
