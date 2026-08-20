using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF (created if missing)
        const string attachmentPath = "secret.bin";       // file to attach (created if missing)
        const string intermediatePath = "temp_attached.pdf"; // PDF after attachment
        const string encryptedPdfPath = "output_encrypted.pdf"; // final encrypted PDF

        const string attachmentDescription = "Encrypted attachment description";

        // User and owner passwords for AES encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // -------------------------------------------------
        // Ensure required files exist – create them on‑the‑fly
        // -------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            // Create a minimal placeholder PDF so the example is self‑contained
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdfPath);
        }

        if (!File.Exists(attachmentPath))
        {
            // Create a simple binary file to act as the attachment
            File.WriteAllBytes(attachmentPath, new byte[] { 0xDE, 0xAD, 0xBE, 0xEF });
        }

        // -------------------------------------------------
        // Step 1: Add attachment to the PDF using PdfContentEditor
        // -------------------------------------------------
        using (var editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);
            // Add attachment with a custom description
            editor.AddDocumentAttachment(attachmentPath, attachmentDescription);
            // Save PDF with the attachment (no encryption yet)
            editor.Save(intermediatePath);
        }

        // -------------------------------------------------
        // Step 2: Encrypt the resulting PDF with AES‑256 using PdfFileSecurity
        // -------------------------------------------------
        using (var security = new PdfFileSecurity())
        {
            security.BindPdf(intermediatePath);
            // Encrypt with AES‑256 (KeySize.x256 + Algorithm.AES)
            security.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256, Algorithm.AES);
            // Save the encrypted PDF
            security.Save(encryptedPdfPath);
        }

        // Clean up intermediate file (optional)
        try { File.Delete(intermediatePath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Attachment added and PDF encrypted successfully: {encryptedPdfPath}");
    }
}
