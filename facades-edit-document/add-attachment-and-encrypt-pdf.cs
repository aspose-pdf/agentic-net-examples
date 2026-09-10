using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, attachment file, and output paths
        const string inputPdf   = "input.pdf";
        const string attachFile = "attachment.txt";
        const string tempPdf    = "temp_with_attachments.pdf";
        const string encryptedPdf = "encrypted_output.pdf";

        // Passwords and security settings
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Ensure input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachFile}");
            return;
        }

        // -------------------------------------------------
        // 1. Add attachment and set viewer preferences
        // -------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPdf);

            // Add a document attachment (no annotation)
            editor.AddDocumentAttachment(attachFile, "Sample attachment");

            // Change viewer preferences (e.g., hide toolbar)
            // ViewerPreference enum is in Aspose.Pdf.Facades namespace
            editor.ChangeViewerPreference((int)ViewerPreference.HideToolbar);

            // Save the modified PDF to a temporary file
            editor.Save(tempPdf);
        }

        // -------------------------------------------------
        // 2. Encrypt the PDF with a user password
        // -------------------------------------------------
        using (PdfFileSecurity security = new PdfFileSecurity(tempPdf, encryptedPdf))
        {
            // Encrypt using AES 256-bit with print permission
            security.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x256);
        }

        // Optional: clean up the intermediate file
        try { File.Delete(tempPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Encrypted PDF saved to '{encryptedPdf}'.");
    }
}