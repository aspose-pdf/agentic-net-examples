using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPath      = "encrypted.pdf";      // Encrypted source PDF
        const string decryptedPath  = "decrypted_temp.pdf"; // Temporary decrypted file
        const string editedPath     = "edited_temp.pdf";    // Temporary edited file
        const string outputPath     = "re_encrypted.pdf";   // Final encrypted PDF

        // Passwords
        const string ownerPassword = "owner123"; // Owner password (required for decryption)
        const string userPassword  = "user123"; // New user password for re‑encryption

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // -------------------------------------------------
        // Decrypt the PDF using PdfFileSecurity (Facades API)
        // -------------------------------------------------
        var decryptor = new PdfFileSecurity(inputPath, decryptedPath);
        bool decryptionSucceeded = decryptor.DecryptFile(ownerPassword);
        if (!decryptionSucceeded)
        {
            Console.Error.WriteLine("Decryption failed.");
            return;
        }

        // -------------------------------------------------
        // Load the decrypted PDF, edit its content, and save
        // -------------------------------------------------
        using (Document doc = new Document(decryptedPath))
        {
            // Example edit: add a text annotation on the first page
            Page page = doc.Pages[1]; // Pages are 1‑based
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            var textAnnotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Edited after decryption",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true
            };
            page.Annotations.Add(textAnnotation);

            // Save the edited PDF to a temporary file
            doc.Save(editedPath);
        }

        // -------------------------------------------------
        // Re‑encrypt the edited PDF using PdfFileSecurity
        // -------------------------------------------------
        var encryptor = new PdfFileSecurity(editedPath, outputPath);
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll; // Allow all operations
        KeySize keySize = KeySize.x256;                         // 256‑bit encryption

        bool encryptionSucceeded = encryptor.EncryptFile(userPassword, ownerPassword, privilege, keySize);
        if (!encryptionSucceeded)
        {
            Console.Error.WriteLine("Encryption failed.");
        }
        else
        {
            Console.WriteLine($"Re‑encrypted PDF saved to '{outputPath}'.");
        }

        // Optional: clean up temporary files
        try { File.Delete(decryptedPath); } catch { }
        try { File.Delete(editedPath); }    catch { }
    }
}