using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths
        const string sourcePdf = "source.pdf";                 // PDF to which the attachment will be added
        const string attachment = "attachment.pdf";            // Original attachment file
        const string encryptedAttachment = "attachment_enc.pdf"; // Encrypted version of the attachment
        const string outputPdf = "output.pdf";                 // Resulting PDF with the encrypted attachment

        // Passwords for encrypting the attachment
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // -------------------------------------------------
        // 1. Prepare dummy source PDF and attachment PDF (sandbox has no files)
        // -------------------------------------------------
        // Create a minimal source PDF
        using (Document srcDoc = new Document())
        {
            srcDoc.Pages.Add();
            srcDoc.Save(sourcePdf);
        }

        // Create a minimal attachment PDF (could be any file type, using PDF for simplicity)
        using (Document attDoc = new Document())
        {
            attDoc.Pages.Add();
            attDoc.Save(attachment);
        }

        // -------------------------------------------------
        // 2. Encrypt the attachment file using PdfFileSecurity (correct overload)
        // -------------------------------------------------
        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        // Bind the source attachment PDF
        fileSecurity.BindPdf(attachment);
        // Encrypt with AES‑256, allowing only printing privilege
        fileSecurity.EncryptFile(
            userPassword,
            ownerPassword,
            DocumentPrivilege.Print,
            KeySize.x256,
            Algorithm.AES);
        // Save the encrypted PDF
        fileSecurity.Save(encryptedAttachment);

        // -------------------------------------------------
        // 3. Add the encrypted attachment to the PDF
        // -------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(sourcePdf);
        // Attach the encrypted file with a description.
        editor.AddDocumentAttachment(encryptedAttachment, "Encrypted attachment");
        editor.Save(outputPdf);
    }
}
