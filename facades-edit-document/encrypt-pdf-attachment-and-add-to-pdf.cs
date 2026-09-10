using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfFileSecurity, PdfContentEditor

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Prepare sample PDFs (the sandbox has no files, so we create them).
        // ---------------------------------------------------------------------
        const string targetPdfPath = "target.pdf";                 // PDF that will receive the attachment
        const string attachmentPdfPath = "attachment.pdf";         // Original attachment (will be encrypted in‑place)
        const string outputPdfPath = "result.pdf";                // Final PDF with encrypted attachment

        // Create a simple target PDF with one blank page.
        using (Document targetDoc = new Document())
        {
            targetDoc.Pages.Add();
            targetDoc.Save(targetPdfPath);
        }

        // Create a simple attachment PDF (also one blank page).
        using (Document attachDoc = new Document())
        {
            attachDoc.Pages.Add();
            attachDoc.Save(attachmentPdfPath);
        }

        // ---------------------------------------------------------------------
        // 2. Encrypt the attachment PDF using the non‑obsolete PdfFileSecurity API.
        // ---------------------------------------------------------------------
        const string userPassword = "user123";   // password required to open the attachment
        const string ownerPassword = "owner123"; // owner password (full permissions)

        // Load the attachment as a Document instance and bind it to PdfFileSecurity.
        using (Document attachmentDoc = new Document(attachmentPdfPath))
        {
            PdfFileSecurity attachmentSecurity = new PdfFileSecurity(attachmentDoc);
            // Encrypt the document in‑place with 256‑bit AES and allow printing.
            attachmentSecurity.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);
            // Save the encrypted version back to the same file path.
            attachmentDoc.Save(attachmentPdfPath);
        }

        // ---------------------------------------------------------------------
        // 3. Add the encrypted attachment to the target PDF.
        // ---------------------------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(targetPdfPath); // Load the PDF to modify

        // Use the overload that accepts a file path (the file is already encrypted).
        editor.AddDocumentAttachment(attachmentPdfPath, "Encrypted attachment");

        editor.Save(outputPdfPath);
        editor.Close(); // Release resources

        Console.WriteLine($"Encrypted attachment added. Output saved to '{outputPdfPath}'.");
    }
}
