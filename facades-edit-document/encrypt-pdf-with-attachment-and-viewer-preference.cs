using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";          // original PDF
        const string attachment    = "attachment.txt";     // file to attach
        const string tempPdf       = "temp_modified.pdf";  // intermediate PDF
        const string encryptedPdf  = "encrypted_output.pdf"; // final encrypted PDF
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Add attachment and set a viewer preference using PdfContentEditor
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the original PDF
            editor.BindPdf(inputPdf);

            // Add a document attachment (no annotation)
            editor.AddDocumentAttachment(attachment, "Sample attachment");

            // Set a viewer preference, e.g., hide the toolbar
            editor.ChangeViewerPreference((int)ViewerPreference.HideToolbar);

            // Save the modified PDF to a temporary file
            editor.Save(tempPdf);
        }

        // -----------------------------------------------------------------
        // 2. Encrypt the modified PDF with a user password using PdfFileSecurity
        // -----------------------------------------------------------------
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Load the temporary PDF
            security.BindPdf(tempPdf);

            // Encrypt with 256‑bit AES, allowing printing
            security.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);

            // Write the encrypted result
            security.Save(encryptedPdf);
        }

        // Clean up the intermediate file (optional)
        try { File.Delete(tempPdf); } catch { }

        Console.WriteLine($"Encrypted PDF saved to '{encryptedPdf}'.");
    }
}