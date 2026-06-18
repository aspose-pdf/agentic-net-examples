using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string intermediatePath   = "temp_modified.pdf";
        const string encryptedPdfPath   = "encrypted_output.pdf";
        const string userPassword       = "user123";
        const string ownerPassword      = ""; // optional owner password

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Add attachment and set viewer preferences
        // ------------------------------------------------------------
        using (Aspose.Pdf.Facades.PdfContentEditor editor = new Aspose.Pdf.Facades.PdfContentEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPdfPath);

            // Add a document attachment (no annotation)
            using (FileStream attStream = File.OpenRead(attachmentFilePath))
            {
                editor.AddDocumentAttachment(
                    attStream,
                    Path.GetFileName(attachmentFilePath), // attachment name
                    "Sample attachment added via PdfContentEditor"); // description
            }

            // Set desired viewer preferences (example: hide menubar and fit window)
            editor.ChangeViewerPreference(Aspose.Pdf.Facades.ViewerPreference.HideMenubar);
            editor.ChangeViewerPreference(Aspose.Pdf.Facades.ViewerPreference.FitWindow);

            // Save the modified PDF to a temporary file
            editor.Save(intermediatePath);
        }

        // ------------------------------------------------------------
        // Step 2: Encrypt the modified PDF with a user password
        // ------------------------------------------------------------
        using (Aspose.Pdf.Facades.PdfFileSecurity security = new Aspose.Pdf.Facades.PdfFileSecurity())
        {
            // Load the intermediate PDF
            security.BindPdf(intermediatePath);

            // Encrypt using 256‑bit AES, allowing only printing (adjust privileges as needed)
            security.EncryptFile(
                userPassword,
                ownerPassword,
                Aspose.Pdf.Facades.DocumentPrivilege.Print,
                Aspose.Pdf.Facades.KeySize.x256,
                Aspose.Pdf.Facades.Algorithm.AES);

            // Persist the encrypted PDF
            security.Save(encryptedPdfPath);
        }

        // Clean up the temporary intermediate file
        try
        {
            File.Delete(intermediatePath);
        }
        catch
        {
            // Ignored – if deletion fails, the file will remain on disk
        }

        Console.WriteLine($"Encrypted PDF saved to '{encryptedPdfPath}'.");
    }
}