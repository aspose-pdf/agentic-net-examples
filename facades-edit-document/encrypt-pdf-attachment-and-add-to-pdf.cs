using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the main PDF, the attachment to be encrypted, and the final output PDF
        const string mainPdfPath      = "main.pdf";
        const string attachmentPath   = "attachment.pdf";
        const string outputPdfPath    = "result.pdf";

        // Passwords for encrypting the attachment
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Ensure the source files exist
        if (!File.Exists(mainPdfPath))
        {
            Console.Error.WriteLine($"Main PDF not found: {mainPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Encrypt the attachment PDF and keep it in memory
        using (var encryptedAttachmentStream = new MemoryStream())
        {
            // Encrypt the attachment using PdfFileSecurity
            using (var attachmentSecurity = new PdfFileSecurity())
            {
                // Bind the original attachment file
                attachmentSecurity.BindPdf(attachmentPath);

                // Encrypt with desired privileges (e.g., allow printing) and 256‑bit key
                attachmentSecurity.EncryptFile(
                    userPassword,
                    ownerPassword,
                    DocumentPrivilege.Print,
                    KeySize.x256);

                // Save the encrypted attachment into the memory stream
                attachmentSecurity.Save(encryptedAttachmentStream);
            }

            // Reset stream position before reading it again
            encryptedAttachmentStream.Position = 0;

            // Add the encrypted attachment to the main PDF
            using (var editor = new PdfContentEditor())
            {
                // Bind the main PDF document
                editor.BindPdf(mainPdfPath);

                // Add the encrypted attachment (no visual annotation, just the file)
                // Parameters: stream, attachment file name, description
                editor.AddDocumentAttachment(
                    encryptedAttachmentStream,
                    "EncryptedAttachment.pdf",
                    "Password‑protected attachment");

                // Save the modified PDF to the output path
                editor.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF with encrypted attachment saved to '{outputPdfPath}'.");
    }
}