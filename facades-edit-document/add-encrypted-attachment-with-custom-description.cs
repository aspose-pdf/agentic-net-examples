using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, attachment file and output PDF paths
        const string inputPdfPath = "input.pdf";
        const string attachmentPath = "attachment.pdf";
        const string outputPdfPath = "output_encrypted.pdf";

        // Passwords for encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Description for the attachment
        const string attachmentDescription = "Custom description for the attached file";

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

        try
        {
            // -----------------------------------------------------------------
            // Step 1: Add the attachment to the PDF using PdfContentEditor
            // -----------------------------------------------------------------
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);
            // Add attachment without annotation, providing description
            editor.AddDocumentAttachment(attachmentPath, attachmentDescription);
            // Save the intermediate PDF (still unencrypted)
            editor.Save(outputPdfPath);
            editor.Close(); // Close the editor (optional, no IDisposable)

            // -----------------------------------------------------------------
            // Step 2: Encrypt the resulting PDF with AES-256 using PdfFileSecurity
            // -----------------------------------------------------------------
            PdfFileSecurity security = new PdfFileSecurity();
            // Bind the PDF that we just saved
            security.BindPdf(outputPdfPath);
            // Encrypt with AES-256 (KeySize.x256 + Algorithm.AES) and allow printing
            bool success = security.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x256,
                Algorithm.AES);

            if (!success)
            {
                Console.Error.WriteLine("Encryption failed.");
                return;
            }

            // Save the encrypted PDF (overwrites the same file)
            security.Save(outputPdfPath);
            security.Close(); // Close the facade

            Console.WriteLine($"Attachment added and PDF encrypted successfully: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}