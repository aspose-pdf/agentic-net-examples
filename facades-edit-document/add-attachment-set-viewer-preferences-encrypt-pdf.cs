using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.txt";
        const string tempPdf = "temp_modified.pdf";
        const string outputPdf = "encrypted_output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = null; // owner password can be null or empty

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Add attachment and set viewer preferences
        // -------------------------------------------------
        var editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Add a document attachment (no visual annotation)
        // Second argument is a description for the attachment
        editor.AddDocumentAttachment(attachmentFile, Path.GetFileName(attachmentFile));

        // Change viewer preferences (example: hide menubar and use no page mode)
        editor.ChangeViewerPreference((int)ViewerPreference.HideMenubar);
        editor.ChangeViewerPreference((int)ViewerPreference.PageModeUseNone);

        // Save the modified PDF to a temporary file
        editor.Save(tempPdf);
        editor.Close();

        // -------------------------------------------------
        // Step 2: Encrypt the temporary PDF with a user password
        // -------------------------------------------------
        var security = new PdfFileSecurity();
        security.BindPdf(tempPdf);

        // Encrypt using AES 256‑bit algorithm, granting only Print privilege
        security.EncryptFile(
            userPassword,
            ownerPassword,
            DocumentPrivilege.Print,
            KeySize.x256,
            Algorithm.AES);

        // Save the encrypted PDF to the final output path
        security.Save(outputPdf);
        security.Close();

        // Clean up the intermediate file
        try { File.Delete(tempPdf); } catch { }

        Console.WriteLine($"Encrypted PDF saved to '{outputPdf}'.");
    }
}