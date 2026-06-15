using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted_output.pdf";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfFileSecurity facade, bind the source PDF, set privileges,
        // apply AES‑256 encryption, and save the encrypted PDF.
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Bind the source PDF document
            security.BindPdf(inputPath);

            // Define custom privileges (example: allow printing but forbid content modification)
            DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
            privilege.AllowPrint = true;               // allow printing
            privilege.AllowModifyContents = false;     // forbid modifying contents
            privilege.AllowCopy = false;               // forbid copying
            // Additional custom settings can be adjusted here

            // Encrypt using AES‑256 (KeySize.x256) and the AES algorithm
            bool encrypted = security.EncryptFile(
                userPassword,
                ownerPassword,
                privilege,
                KeySize.x256,
                Algorithm.AES);

            if (!encrypted)
            {
                Console.Error.WriteLine("Encryption failed.");
                return;
            }

            // Save the encrypted PDF to the specified output path
            security.Save(outputPath);
        }

        Console.WriteLine($"PDF encrypted with AES‑256 and custom privileges saved to '{outputPath}'.");
    }
}