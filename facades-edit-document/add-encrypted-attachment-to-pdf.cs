using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF to which the attachment will be added
        const string inputPdfPath = "input.pdf";
        // File to be attached
        const string attachmentPath = "secret.docx";
        // Custom description for the attachment
        const string attachmentDescription = "Confidential document - encrypted attachment";
        // Temporary PDF after adding the attachment (before encryption)
        const string tempPdfPath = "temp_with_attachment.pdf";
        // Final encrypted PDF output
        const string outputPdfPath = "encrypted_output.pdf";

        // Passwords for encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Ensure input files exist
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

        // ------------------------------------------------------------
        // Step 1: Add the attachment with a custom description
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdfPath);

            // Add the attachment (no annotation) with the provided description
            editor.AddDocumentAttachment(attachmentPath, attachmentDescription);

            // Save the intermediate PDF that now contains the attachment
            editor.Save(tempPdfPath);
        }

        // ------------------------------------------------------------
        // Step 2: Encrypt the PDF (including the attachment) using AES-256
        // ------------------------------------------------------------
        using (Document doc = new Document(tempPdfPath))
        {
            // Define permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt with AES-256 algorithm
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the final encrypted PDF
            doc.Save(outputPdfPath);
        }

        // Clean up the temporary file
        try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Encrypted PDF with attachment saved to '{outputPdfPath}'.");
    }
}