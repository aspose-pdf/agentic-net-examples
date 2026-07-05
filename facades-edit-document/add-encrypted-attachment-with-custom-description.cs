using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for source PDF, attachment, intermediate and final files
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.pdf";
        const string attachmentDescription = "Custom description for attachment";
        const string tempPdfWithAttachment = "temp_with_attachment.pdf";
        const string encryptedPdf = "encrypted_output.pdf";

        // Passwords for encryption (AES will be used)
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify that required files exist
        if (!File.Exists(inputPdf) || !File.Exists(attachmentFile))
        {
            Console.Error.WriteLine("Input PDF or attachment file not found.");
            return;
        }

        // -------------------------------------------------
        // Step 1: Add the attachment to the PDF (no annotation)
        // -------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);                                   // Load the source PDF
            editor.AddDocumentAttachment(attachmentFile, attachmentDescription); // Add attachment with description
            editor.Save(tempPdfWithAttachment);                         // Save intermediate PDF
        }

        // -------------------------------------------------
        // Step 2: Encrypt the PDF (including the attachment) using AES‑256
        // -------------------------------------------------
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            security.BindPdf(tempPdfWithAttachment);                    // Load PDF that now contains the attachment
            // Encrypt with user/owner passwords, allow printing, AES‑256 algorithm
            security.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256, Algorithm.AES);
            security.Save(encryptedPdf);                                // Save the encrypted PDF
        }

        Console.WriteLine($"Encrypted PDF with attachment saved to '{encryptedPdf}'.");
    }
}