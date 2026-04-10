using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";          // source PDF
        const string attachmentPdf = "attachment.docx";    // file to attach
        const string tempPdf       = "temp_modified.pdf"; // intermediate file
        const string encryptedPdf  = "encrypted_output.pdf";
        const string userPassword  = "user123";

        // Ensure source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPdf))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Add attachment and set viewer preferences using PdfContentEditor
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file
            editor.BindPdf(inputPdf);

            // Add a document attachment (no annotation)
            editor.AddDocumentAttachment(attachmentPdf, "Sample attachment");

            // Change viewer preferences – hide toolbar as an example
            // ViewerPreference enum resides in Aspose.Pdf.Facades namespace
            editor.ChangeViewerPreference((int)ViewerPreference.HideToolbar);

            // Save the modified PDF to a temporary file
            editor.Save(tempPdf);
        }

        // -----------------------------------------------------------------
        // 2. Encrypt the temporary PDF with a user password using PdfFileSecurity
        // -----------------------------------------------------------------
        using (PdfFileSecurity security = new PdfFileSecurity(tempPdf, encryptedPdf))
        {
            // Encrypt with user password, no owner password, allow printing, 256‑bit AES
            // DocumentPrivilege and KeySize are defined in Aspose.Pdf namespace
            security.EncryptFile(
                userPassword,               // user password
                null,                       // owner password (null => random)
                DocumentPrivilege.Print,    // allowed privilege
                KeySize.x256);              // 256‑bit key size
        }

        // Cleanup intermediate file
        try { File.Delete(tempPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Encrypted PDF saved to '{encryptedPdf}'.");
    }
}