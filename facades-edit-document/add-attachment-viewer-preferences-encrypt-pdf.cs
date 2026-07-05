using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, temporary PDF (after modifications), and final encrypted PDF
        const string inputPdf      = "input.pdf";
        const string tempPdf       = "temp_modified.pdf";
        const string encryptedPdf  = "encrypted.pdf";

        // User password for encryption
        const string userPassword = "user123";

        // ------------------------------------------------------------
        // 1. Ensure the source PDF and attachment file exist (self‑contained example)
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add();
                seed.Save(inputPdf);
            }
        }

        const string attachmentPath = "sample.txt";
        if (!File.Exists(attachmentPath))
        {
            File.WriteAllText(attachmentPath, "This is a sample attachment file.");
        }

        // ------------------------------------------------------------
        // 2. Add an attachment and set viewer preferences using PdfContentEditor
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPdf);

            // Add a file attachment (no visual annotation)
            // Parameters: filePath, description
            editor.AddDocumentAttachment(attachmentPath, "Sample attachment");

            // Change viewer preferences.
            // ViewerPreference enum values are defined in Aspose.Pdf.Facades.
            // Example: hide the toolbar (value 1). Adjust as needed.
            editor.ChangeViewerPreference((int)ViewerPreference.HideToolbar);

            // Save the modified PDF to a temporary file
            editor.Save(tempPdf);
        }

        // ------------------------------------------------------------
        // 3. Encrypt the temporary PDF with a user password using PdfFileSecurity
        // ------------------------------------------------------------
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Load the PDF that was just modified
            security.BindPdf(tempPdf);

            // Encrypt the document:
            // - userPassword: the password required to open the PDF
            // - ownerPassword: null (Aspose will generate a random owner password)
            // - privilege: allow printing (other privileges can be combined)
            // - keySize: 256‑bit encryption (AES is used by default for this key size)
            security.EncryptFile(userPassword, null, DocumentPrivilege.Print, KeySize.x256);

            // Save the encrypted PDF to the final output file
            security.Save(encryptedPdf);
        }
    }
}
